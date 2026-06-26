using System;
using UnityEngine;
using Utilities;

public class OnHitEventArgs : EventArgs
{
    public HitInfo HitInfo;
    public Collider Collider;
}

[RequireComponent(typeof(CapsuleCollider), typeof(Rigidbody))]
public class BulletBehavior : MonoBehaviour
{
    // Debug
    [Header("TEST Parameters")]
    [SerializeField] private bool useTestParameters = false;
    [SerializeField] private float speed;
    [SerializeField] private float damage;
    [SerializeField] private int maxNumOfObjectToPirce;

    [Header("SO")]
    [SerializeField] private BulletDataSO bulletDataSO;
    private int currentPirce;

    [Header("Pool")]
    public BulletPool Pool;
    
    // Multiplayer
    private float damageMultiplayer = 1.0f;
    public float DamageMultiplayer
    {
        get { return damageMultiplayer; }
        set { damageMultiplayer = value; }
    }
    private float speedMultiplayer = 1.0f;
    public float SpeedMultiplayer
    {
        get { return speedMultiplayer; }
        set { speedMultiplayer = value; }
    }
    
    private Rigidbody rigidBody;
    private CallOutOfRange outOfrange;

    internal bool bIsReleased = true;

    #region bullet VFX 
    [Header("VFX Prefabs")]
    private GameObject defaultImpactVFX;
    private GameObject impactVFX;
    public GameObject ImpactVFX
    {
        get => impactVFX;
        set => impactVFX = value;
    }
    
    private GameObject defaultTrailVFX;
    private GameObject trailVFX;
    public GameObject TrailVFX
    {
        get => trailVFX;
        set => trailVFX = value;
    }

    // Dynamic Runtime References
    private GameObject activeTrailInstance; 
    private TrailRenderer cachedTrailRenderer;
    #endregion

    // Events
    public event EventHandler<EventArgs> OnInstantiate;
    public event EventHandler<EventArgs> OnTraveling;
    public event EventHandler<OnHitEventArgs> OnHitTrigger;

    private bool isStartingTraveling;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
        outOfrange = GetComponent<CallOutOfRange>();
    }
    
    private void OnEnable()
    {
        isStartingTraveling = true;
        outOfrange.OnOutOfRange += OutOfrange_OnOutOfRange;
        bIsReleased = false;

        ResetTrailForPooling();
    }

    private void OnDisable()
    {
        bIsReleased = true;
        outOfrange.OnOutOfRange -= OutOfrange_OnOutOfRange;
    
        if (activeTrailInstance != null)
        {
            // 1. Force the trail to stop generating any visual path geometry
            if (cachedTrailRenderer != null)
            {
                cachedTrailRenderer.emitting = false;
            }
            activeTrailInstance.SetActive(false);
        }
    }

    private void OutOfrange_OnOutOfRange()
    {
        if(!bIsReleased)
            Pool.Release(this);
    }

    private void Start()
    {
        rigidBody.isKinematic = true;
        rigidBody.useGravity = false;

        if (!useTestParameters)
        {
            speed = bulletDataSO.BaseSpeed;
            damage = bulletDataSO.BaseDamage;
            maxNumOfObjectToPirce = bulletDataSO.BaseNumObjectToPirce;
        }

        currentPirce = maxNumOfObjectToPirce;
        
        defaultImpactVFX = EffectsManager.I.NormalHitVfxPrefab;
        impactVFX = defaultImpactVFX;
        
        defaultTrailVFX = EffectsManager.I.NormalTrailPrefab;
        trailVFX = defaultTrailVFX;

        InitializeTrailInstance();
    }

    private void InitializeTrailInstance()
    {
        if (activeTrailInstance != null) return;

        GameObject archetype = defaultTrailVFX;
        if (TryGetComponent<BaseModifier>(out BaseModifier baseModifier) && baseModifier.isActiveAndEnabled)
        {
            archetype = trailVFX;
        }

        if (archetype != null)
        {
            activeTrailInstance = Instantiate(archetype, this.transform);
            activeTrailInstance.transform.localPosition = Vector3.zero;
            activeTrailInstance.transform.localRotation = Quaternion.identity;
            
            cachedTrailRenderer = activeTrailInstance.GetComponent<TrailRenderer>();
        }
    }

    private void ResetTrailForPooling()
    {
        if (activeTrailInstance == null)
        {
            InitializeTrailInstance();
        }

        if (activeTrailInstance != null)
        {
            activeTrailInstance.SetActive(true);
        
            if (cachedTrailRenderer != null)
            {
                // 2. Clear out any residual world-space position history
                cachedTrailRenderer.Clear();
            
                // 3. Keep it completely disabled for an instant split-second
                cachedTrailRenderer.emitting = false;
            
                // 4. Safely tell the trail to start rendering again, starting *from* the new position
                Invoke(nameof(EnableTrailEmission), 0.01f);
            }
        }
    }
    
    private void EnableTrailEmission()
    {
        if (cachedTrailRenderer != null && !bIsReleased)
        {
            cachedTrailRenderer.emitting = true;
        }
    }

    private void FixedUpdate()
    {
        if (isStartingTraveling)
        {
            OnInstantiate?.Invoke(this, EventArgs.Empty);
            isStartingTraveling = false;
        }

        OnTraveling?.Invoke(this, EventArgs.Empty);
        rigidBody.MovePosition(rigidBody.transform.position + rigidBody.transform.forward * (speed * SpeedMultiplayer * Time.fixedDeltaTime));
    }

    private void OnTriggerEnter(Collider other)
    {
        // Evaluate the damage
        HealthEffect healthEffect = new HealthEffect
        {
            Amount = EvalBulletDamage(),
            Type = HealthEffectType.Damage
        };

        other.GetComponent<IHealthReceiver>()?.ApplyEffect(healthEffect); 
        
        Vector3 impactPoint = transform.position;
        Ray ray = new Ray(transform.position, -transform.forward);
        if (other.Raycast(ray, out RaycastHit hit, 3f))
        {
            impactPoint = hit.point;
        }
        
        OnHitTrigger?.Invoke(this, new OnHitEventArgs 
        {
            HitInfo = new HitInfo
            {
                HitPoint = impactPoint,
            },
            Collider = other
        });
        
        // VFX Hit Spawn Handling
        GameObject vfxTemplate = defaultImpactVFX;
        if (TryGetComponent<BaseModifier>(out BaseModifier baseModifier) && baseModifier.isActiveAndEnabled)
        {
            vfxTemplate = ImpactVFX;
        }

        if (vfxTemplate != null)
        {
            GameObject vfxInstance = Instantiate(vfxTemplate, impactPoint, Quaternion.identity);
            Destroy(vfxInstance, 1f);
        }
        
        if (currentPirce <= 0) 
        {
            Pool.Release(this);
            return;
        }
        else
        {
            currentPirce -= 1;
        }
    }

    public void IncreaseNumOfObjectToPirce(int amount)
    {
        maxNumOfObjectToPirce += amount;
        currentPirce = maxNumOfObjectToPirce;
    }

    public void IncreaseDamage(float amount)
    {
        if (amount <= 0) return;
        damage += amount;
    }
    
    public void IncreaseSpeed(float amount)
    {
        if (amount <= 0) return;
        speed += amount;
    }

    private float EvalBulletDamage()
    {
        return damage * DamageMultiplayer; 
    }
}