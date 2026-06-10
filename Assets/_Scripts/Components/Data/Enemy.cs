using System;
using UnityEngine;

/// <summary>
/// This class is used to handle enemy data an initialization.
/// </summary>
public class Enemy : MonoBehaviour
{
    [SerializeField] private EnemyData enemyData;
    public int DroppedExp => enemyData.xpValue;
    
    private void OnEnable()
    {
        gameObject.GetComponent<HealthComponent>().Death += OnDeath;
    }
    
    private void OnDisable()
    {
        gameObject.GetComponent<HealthComponent>().Death -= OnDeath;
    }

    private void OnDeath(GameObject obj)
    {
        CombatEvents.OnEnemyKilled?.Invoke(DroppedExp);
    }
}
