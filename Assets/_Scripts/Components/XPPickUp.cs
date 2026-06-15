using UnityEngine;

/// <summary>
/// An Object that adds Xp When picked up
/// </summary>
public class XPPickUp : PickUp
{
    private int value;
    internal int Value { get; set; }
    
    public override void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Player")) return;
        Interact();
        //Do something else?
    }

    public override void Interact()
    {
        CombatEvents.OnExperiencePickUp(this);
        //Debug.Log("Picking up XP.");
        Destroy(gameObject);
    }

    public void Launch(Vector3 launchVelocity)
    {
        rb.linearVelocity = launchVelocity;
    }
}
