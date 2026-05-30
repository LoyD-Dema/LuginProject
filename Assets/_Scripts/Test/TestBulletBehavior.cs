using System;
using UnityEngine;

public class TestBulletBehavior : MonoBehaviour
{
    [SerializeField] private BulletBehavior bulletBehavior;
    [SerializeField] private GameObject spawnPoint;
    [Range(0.5f, 5.0f)]
    [SerializeField] private float delaySpawn;
    [SerializeField] private bool spawn;

    private Vector3 spawnPos;
    private Quaternion rotation;

    private void Start()
    {
        if (spawn)
        {
            InvokeRepeating(nameof(Spawn), delaySpawn, delaySpawn);
        }
    }

    private void Spawn()
    {
        if (spawnPoint == null)
        {
            spawnPos = new Vector3(0, 2, 0);
            rotation = Quaternion.identity;
        }
        else
        {
            spawnPos = spawnPoint.transform.position;
            rotation = spawnPoint.transform.rotation;
        }

        BulletBehavior b = Instantiate(bulletBehavior, spawnPos, rotation);
        b.OnInstantiate += Bullet_OnInstantiate;
        b.OnTraveling += Bullet_OnTraveling;
        b.OnHit += Bullet_OnHit;

        b.enabled = false;
        b.enabled = true;
    }

    private void Bullet_OnHit(object sender, BulletBehavior.OnHitEventArgs e)
    {
        if (sender is BulletBehavior bulletBehavior)
        {
            Debug.Log($"{bulletBehavior} hitted {e.enemy}", bulletBehavior);
        }
    }

    private void Bullet_OnTraveling(object sender, EventArgs e)
    {
        if (sender is BulletBehavior bulletBehavior)
        {
            Debug.Log($"{bulletBehavior} is traveling", bulletBehavior);
        }
    }

    private void Bullet_OnInstantiate(object sender, EventArgs e)
    {
        if(sender is BulletBehavior bulletBehavior)
        {
            Debug.Log($"{bulletBehavior} has instanced", bulletBehavior);
        }
    }


}

