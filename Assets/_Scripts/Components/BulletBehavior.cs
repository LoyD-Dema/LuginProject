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
    private TrailRenderer bulletTrail;
    
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

    [SerializeField] private GameObject defaultImpactVFX;
    private GameObject impactVFX;
    public GameObject ImpactVFX
    {
        get { return impactVFX; }
        set { impactVFX = value; }
    }

    // Events
    public event EventHandler<EventArgs> OnInstantiate;
    public event EventHandler<EventArgs> OnTraveling;
    public event EventHandler<OnHitEventArgs> OnHitTrigger;

    private bool isStartingTraveling;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
        outOfrange = GetComponent<CallOutOfRange>();
        bulletTrail = GetComponentInChildren<TrailRenderer>();
    }
    
    private void OnEnable()
    {
        isStartingTraveling = true;
        outOfrange.OnOutOfRange += OutOfrange_OnOutOfRange;
        bIsReleased = false;
    }

    private void OnDisable()
    {
        bulletTrail.Clear();
        bIsReleased = true;
        outOfrange.OnOutOfRange -= OutOfrange_OnOutOfRange;
        var c = GetComponent<BaseModifier>();
    }

    private void OutOfrange_OnOutOfRange()
    {
        if(!bIsReleased)
            Pool.Relese(this);
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
        //evaluate the damage
        HealthEffect healthEffect = new HealthEffect
        {
            Amount = EvalBulletDamage(),
            Type = HealthEffectType.Damage
        };

        other.GetComponent<IHealthReceiver>()?.ApplyEffect(healthEffect); //apply hit effects
        Vector3 impactPoint = other.ClosestPoint(transform.position); //for the VFX position
        
        OnHitTrigger?.Invoke(this, new OnHitEventArgs //spatial information about the collision
        {
            HitInfo = new HitInfo
            {
                HitPoint = impactPoint,
            },
            Collider = other
        });
        
        GameObject vfx;
        if (TryGetComponent<BaseModifier>(out BaseModifier baseModifier))
        {
            Debug.Log(baseModifier);
            if(baseModifier.isActiveAndEnabled)
                vfx = Instantiate(ImpactVFX, impactPoint , Quaternion.identity);
            else
                vfx = Instantiate(defaultImpactVFX, impactPoint , Quaternion.identity);
        }
        else
            vfx = Instantiate(defaultImpactVFX, impactPoint , Quaternion.identity);

        Destroy(vfx, 1f);
       
        if (currentPirce == 0) //evaluate potential piercing TODO: check this. Should apply partial piercing damage?
        {
            Pool.Relese(this);
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
        if (amount <= 0)
            return;

        damage += amount;
    }
    public void IncreaseSpeed(float amount)
    {
        if (amount <= 0)
            return;
        speed += amount;
    }

    private float EvalBulletDamage()
    {
        return damage * DamageMultiplayer; //moved it to a function if we want to make it more complex
    }
}
