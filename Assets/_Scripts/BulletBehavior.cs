using System;
using UnityEngine;

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
        set
        {
            damageMultiplayer = Mathf.Max(1.0f, value);
        }
    }

    private float speedMultiplayer = 1.0f;
    public float SpeedMultiplayer
    {
        get { return speedMultiplayer; }
        set
        {
            speedMultiplayer = Mathf.Max(1.0f, value);
        }
    }


    private Rigidbody rigidBody;
    
    // Events
    public event EventHandler<EventArgs> OnInstantiate;
    public event EventHandler<EventArgs> OnTraveling;
    public event EventHandler<OnHitEventArgs> OnHit;

    public class OnHitEventArgs : EventArgs
    {
        public GameObject enemy;
    }

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
        if(other.CompareTag("Enemy"))
        {
            OnHit?.Invoke(this, new OnHitEventArgs { enemy = other.gameObject });
            // TODO - Add damage to the enemy
            float actualDamage = damage * DamageMultiplayer;
            Debug.Log($"Damage: {actualDamage} applied to {other.gameObject.name}");

            if(currentPirce == 0)
            {
                /* TODO - Put back to the pool
                 * delete Destroy()
                 */
                Destroy(gameObject);
            }
            else
            {
                currentPirce -= 1;
            }
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
}
