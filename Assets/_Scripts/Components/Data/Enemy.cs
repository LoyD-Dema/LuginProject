using System;
using UnityEngine;

/// <summary>
/// This class is used to handle enemy data an initialization.
/// </summary>
public class Enemy : MonoBehaviour
{
    [SerializeField] private EnemyData enemyData;
    [SerializeField] private XPDrop XpDrop;
    
    public int DroppedExp => enemyData.xpValue;
    
    private void OnEnable()
    {
        HealthComponent.Death += OnDeath;
    }
    
    private void OnDisable()
    {
        HealthComponent.Death -= OnDeath;
    }

    private void Start()
    {
        XpDrop = gameObject.GetComponentInChildren<XPDrop>();
    }

    private void OnDeath(GameObject obj)
    {
        CombatEvents.OnEnemyKilled?.Invoke(DroppedExp);

        if(obj!=gameObject) return;
        //call first the component that drops the experience.
        XpDrop.Drop(DroppedExp, transform.position);
    }
}
