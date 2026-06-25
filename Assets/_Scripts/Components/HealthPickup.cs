using UnityEngine;

public class HealthPickup : PickUp
{
    [SerializeField]
    private int recoveryAmount = 10;

    public int RecoveryAmount { get { return recoveryAmount; } }

    public override void OnTriggerEnter(Collider other)
    {
        Debug.Log("getting picked up");
        if (!other.gameObject.CompareTag("Player")) return;
        Interact();
    }

    public override void Interact()
    {
        CombatEvents.OnHealthPickup(this);
    }
}
