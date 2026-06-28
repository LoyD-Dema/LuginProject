using UnityEngine;

public class HealthInteractionComponent : MonoBehaviour
{
    private void OnEnable()
    {
        CombatEvents.OnHealthPickup += AddHealth;
    }

    private void OnDisable()
    {
        CombatEvents.OnHealthPickup -= AddHealth;
    }

    private void AddHealth(HealthPickup pickUp)
    {
        GetComponent<HealthComponent>().GainHealth(pickUp.RecoveryAmount);
        Destroy(pickUp.gameObject);
    }
}
