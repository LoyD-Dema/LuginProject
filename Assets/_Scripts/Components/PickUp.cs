using UnityEngine;

/// <summary>
/// An object that can be interacted with and picked up.
/// </summary>
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(SphereCollider))]
public abstract class PickUp:MonoBehaviour, IInteractable
{
    internal Rigidbody rb;
    [SerializeField] internal SphereCollider interactionCollider;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        interactionCollider = GetComponent<SphereCollider>();
    }

    public abstract void OnTriggerEnter(Collider other);
    public abstract void Interact();
}
