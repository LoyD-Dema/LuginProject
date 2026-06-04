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

    public void ReceiveHit(HitInfo hitInfo)
    {
        HitReceived?.Invoke(hitInfo);
    }
}
