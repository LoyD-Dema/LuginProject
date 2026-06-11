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
    
    // Multiplayer
    private float damageMultiplayer = 1.0f;
    public float DamageMultiplayer
    {
        get { return damageMultiplayer; }
        set { damageMultiplayer = Mathf.Max(1.0f, value); }
    }
    private float speedMultiplayer = 1.0f;
    public float SpeedMultiplayer
    {
        get { return speedMultiplayer; }
        set { speedMultiplayer = Mathf.Max(1.0f, value); }
    }
    
    private Rigidbody rigidBody;

    private Vector3 previousPosition;
    private Vector3 impactPoint;
    
    // Events
    public event EventHandler<EventArgs> OnInstantiate;
    public event EventHandler<EventArgs> OnTraveling;
    public event EventHandler<OnHitEventArgs> OnHit;

    private bool isStartingTraveling;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        OnInstantiate?.Invoke(this,  EventArgs.Empty);
        isStartingTraveling = true;
    }

    private void Start()
    {
        rigidBody.isKinematic = true;
        rigidBody.useGravity = false;

        if(!useTestParameters)
        {
            speed = bulletDataSO.BaseSpeed;
            damage = bulletDataSO.BaseDamage;
            maxNumOfObjectToPirce = bulletDataSO.BaseNumObjectToPirce;
        }

        currentPirce = maxNumOfObjectToPirce;
        
        previousPosition = transform.position;
    }

    private void Update()
    {
       
    }
    
    private void FixedUpdate()
    {
        if (isStartingTraveling)
        {
            OnTraveling?.Invoke(this, EventArgs.Empty);
            isStartingTraveling = false;
        }

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
        impactPoint = other.ClosestPoint(impactPoint); //for the VFX position

        OnHit?.Invoke(this, new OnHitEventArgs //spatial information about the collision
        {
            HitInfo = new HitInfo
            {
                HitPoint = impactPoint,
            },
            Collider = other
        });
        
        if(currentPirce == 0) //evaluate potential piercing TODO: check this. Should apply partial piercing damage?
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            currentPirce -= 1;
        }
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
