using System.Collections;
using UnityEngine;

/// <summary>
/// An Object that adds Xp When picked up
/// </summary>
public class XPPickUp : PickUp
{
    private int value;
    internal int Value { get; set; }
    
    private Vector3 attractionTarget;
    private bool bIsBeingAttracted = false;
    private bool bIsReleased = true;
    
    [SerializeField][Range(1,50)] private float attractionForce = 10f;
    [SerializeField][Range(1,60*2)] private float secondsToDespawn = 3;
    private XPDrop originObj;

    private Coroutine despawnRoutine;

    private void OnEnable()
    {
        bIsReleased = false;
        
        if(despawnRoutine != null)
            StopCoroutine(despawnRoutine);
        
        despawnRoutine = StartCoroutine(AutoDespawn());
    }

    private void OnDisable()
    {
        bIsReleased = true;
    }
    
    public void Launch(XPDrop originObj, Vector3 launchVelocity)
    {
        this.originObj = originObj;
        rb.linearVelocity = launchVelocity;
    }

    public void BeginAttract(Vector3 target)
    {
        attractionTarget = target;
        bIsBeingAttracted = true;
    }

    private void FixedUpdate()
    {
        if (!bIsBeingAttracted) return;
        rb.linearVelocity = (attractionTarget-transform.position).normalized * attractionForce;
    }
    
    public override void OnTriggerEnter(Collider other)
    {
        //Debug.Log("getting picked up");
        if (!other.gameObject.CompareTag("Player")) return;
        Interact();
        //Do something else?
    }
    
    public override void Interact()
    {
        CombatEvents.OnExperiencePickUp(this);
        //Debug.Log("Picking up XP.");
        this.rb.linearVelocity = Vector3.zero; //reset the velocity
        originObj.xpDroppablesPool.Release(this);
    }

    private IEnumerator AutoDespawn()
    {
        yield return new WaitForSeconds(secondsToDespawn);

        bIsBeingAttracted = false;
        if (!bIsReleased)
            originObj.xpDroppablesPool.Release(this);
    }
}
