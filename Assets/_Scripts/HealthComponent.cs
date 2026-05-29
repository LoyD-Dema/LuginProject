using System;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    [SerializeField, Min(0f)] private float maxHealth;
    private float currentHealth;
    private bool isDead;
    
    private event Action onMaxHealthChanged; //in caso vogliamo modificare la salute massima in runtime
    private event Action onHeal;
    private event Action onDamage;
    private event Action onDeath;
    
    private void Awake()
    {
        currentHealth = maxHealth;
        onMaxHealthChanged += () =>
        {
            if (currentHealth > maxHealth)
                currentHealth = maxHealth;
        };
        
        isDead = false;
    }
  
    private void TakeDamage(float damage)
    {
        currentHealth -= damage;
        onDamage?.Invoke();
        if (currentHealth <= 0)
            Die();
    }
    
    private void GainHealth(float amount)
    {
        currentHealth += amount;
        onHeal?.Invoke();
        if (currentHealth > maxHealth)
            currentHealth = maxHealth;
    }

    private void Die()
    {
        currentHealth = 0;
        onDeath?.Invoke();
    }
    
}
