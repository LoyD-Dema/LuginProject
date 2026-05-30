using System;
using UnityEngine;

using Utilities;
/// <summary>
/// Represents an actor in the game, which can be a player, an enemy, or any other entity that can interact with the environment and other actors.
/// </summary>

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]

[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(Rigidbody))]
public class Actor : MonoBehaviour
{
    protected MeshFilter MeshFilter;
    protected MeshRenderer MeshRenderer;
    protected CapsuleCollider CapsuleCollider;
    protected Rigidbody RigidBody;

    public event Action<HitInfo> HitReceived;    
    
    private void Awake()
    {
        MeshFilter = GetComponent<MeshFilter>();
        MeshRenderer = GetComponent<MeshRenderer>();
        CapsuleCollider = GetComponent<CapsuleCollider>();
        RigidBody = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            //TODO: here i made it like this but this struct can of course be passed by the projectile and contain more information
            HitReceived?.Invoke(new HitInfo
            {
                Damage = 1
            });
            Debug.Log("Collided with a bullet!");
        }
    }
    
}
