using UnityEngine;

/// <summary>
/// An Object that adds Xp When picked up
/// </summary>
public class XPPickUp : PickUp
{
    private int value;
    internal int Value { get; set; }
    private XPDrop originObj;
    
    public void Launch(XPDrop originObj, Vector3 launchVelocity)
    {
        this.originObj = originObj;
        rb.linearVelocity = launchVelocity;
    }
    
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
        originObj.xpDroppablesPool.Release(this);
    }
}
