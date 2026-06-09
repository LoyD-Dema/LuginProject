using System;
using Unity.VisualScripting;
using UnityEngine;
using Utilities;

/// <summary>
/// Provides basic health management for a game object, including taking damage, healing, and death handling.
/// </summary>
public class HealthComponent : MonoBehaviour, IHealthReceiver
{
    #region Stats
    [SerializeField, Min(0f)] private float maxHealth = 100f;
    public float MaxHealth => maxHealth;
    public float CurrentHealth { get; private set; }
    public bool IsDead { get; private set; }
    #endregion
    
    #region Events of the HealthComponent
    private event Action MaxHealthChanged; //in caso vogliamo modificare la salute massima in runtime
    public event Action Heal; //Ho messo questo pubblico cos� posso gestire anche la cura tramite l'evento altrimenti non potevo usarlo
    public event Action Damage;
    public event Action<GameObject> Death;
    #endregion
    
    private void Awake()
    {
        CurrentHealth = maxHealth;
        IsDead = false;
    }
    
    private void Start()
    {
        Debug.Log($"HealthComponent initialized with ExpToNextLvl: {maxHealth} and CurrentExp: {CurrentHealth}");
    }

    private void OnEnable()
    {
        IsDead = false;
        CurrentHealth = maxHealth;    
    }
    
    // Allows external systems to modify the maximum health of the component at runtime
    //TODO: I don't like this level of control that we give to external systems,maybe we can add some checks or conditions to prevent abuse of this method
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

        if (gameObject.tag == "Enemy")
        {
            Debug.Log($"{gameObject.name} - Current health: {CurrentHealth}");
        }
        
        if (CurrentHealth <= 0)
            Die();
    }
    
    public void GainHealth(float amount)
    {
        CurrentHealth += amount;
        
        if (CurrentHealth > maxHealth)
            CurrentHealth = maxHealth;
        
        Heal?.Invoke();
        Debug.Log($"{gameObject.name} - Current health: {CurrentHealth}");
    }
    
    private void Die()
    {
        CurrentHealth = 0;
        IsDead = true;
        
        Debug.Log("I am dead");
        
        Death?.Invoke(gameObject);
    }

    public void ApplyEffect(HealthEffect effect)
    {
        switch (effect.Type)
        {
            case HealthEffectType.Heal:
                GainHealth(effect.Amount);
                break;
            case HealthEffectType.Damage:
                TakeDamage(effect.Amount);
                break;
        }
    }
}
