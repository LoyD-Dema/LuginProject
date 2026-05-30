using System;
using UnityEngine;
using Utilities;

/// <summary>
/// Provides basic health management for a game object, including taking damage, healing, and death handling.
/// </summary>
public class HealthComponent : MonoBehaviour
{
    private Actor owningActor;
    
    [SerializeField, Min(0f)] private float maxHealth = 100f; 
    private float CurrentHealth { get; set; }
    private bool IsDead { get; set; }
    
    private event Action MaxHealthChanged; //in caso vogliamo modificare la salute massima in runtime
    private event Action Heal;
    private event Action Damage;
    private event Action Death;
    
    private void Awake()
    {
        CurrentHealth = maxHealth;
        IsDead = false;
    }

    private void OnEnable()
    {
        owningActor = GetComponent<Actor>();
        owningActor.HitReceived += OnHitReceived;
    }

    //dispatches the information about the hit
    private void OnHitReceived(HitInfo hitInfo)
    {
        TakeDamage(hitInfo.Damage);
    }

    private void Start()
    {
        Debug.Log($"HealthComponent initialized with MaxHealth: {maxHealth} and CurrentHealth: {CurrentHealth}");
    }

    public void SetMaxHealth(float newMaxHealth)
    {
        maxHealth = newMaxHealth;
        MaxHealthChanged?.Invoke();
    }
  
    public void TakeDamage(float damage)
    {
        if (IsDead) return;
        
        CurrentHealth -= damage;
        Damage?.Invoke();
        
        Debug.Log($"Current health: {CurrentHealth}");
        if (CurrentHealth <= 0)
            Die();
    }
    
    public void GainHealth(float amount)
    {
        CurrentHealth += amount;
        
        if (CurrentHealth > maxHealth)
            CurrentHealth = maxHealth;
        
        Heal?.Invoke();
        Debug.Log($"Current health: {CurrentHealth}");
    }

    private void OnDisable()
    {
        owningActor.HitReceived -= OnHitReceived;
    }
    
    private void Die()
    {
        CurrentHealth = 0;
        IsDead = true;
        
        Debug.Log("I am dead");
        
        Death?.Invoke();
    }
    
}
