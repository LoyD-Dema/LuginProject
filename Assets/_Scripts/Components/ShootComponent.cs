using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShootComponent : MonoBehaviour
{
    [Range(0.1f, 5.0f)]
    [SerializeField] float fireRate = 0.5f;
    [Range(0f, 5.0f)]
    [SerializeField] float distanceMultiplayer;
    private float elapsedFireRateTime;

    // TODO - Change with the BulletBehavior Class
    [SerializeField] GameObject bullet;
    [SerializeField] Transform bulletSpawn;

    private Camera camera = null;
    private Vector3 aimGroundLocation;

    private void Start()
    {
        elapsedFireRateTime = 0;
        if (gameObject.CompareTag("Player"))
        {
            camera = Camera.main;
        }
    }

    public void Shoot()
    {
        if (elapsedFireRateTime >= 0) return;

        Vector3 bulletSpawnDirectionNormalized = bulletSpawn.forward;
        Quaternion bulletSpawnRotation = bulletSpawn.rotation;
        
        // TODO - Change it by taking the bullet from the pool
        if (gameObject.CompareTag("Player"))
        {
            bulletSpawnDirectionNormalized = (aimGroundLocation - bulletSpawn.position).normalized;
            bulletSpawnRotation = Quaternion.LookRotation(aimGroundLocation - bulletSpawn.position);
        }
        
        Instantiate(bullet, bulletSpawn.position + bulletSpawnDirectionNormalized, bulletSpawnRotation);
        elapsedFireRateTime = fireRate;
    }

    private void Update()
    {
        //Evaluate shooting direction depending on cursor position if I am the player
        if (gameObject.CompareTag("Player"))
        {
            Ray ray = camera.ScreenPointToRay(Mouse.current.position.ReadValue());
            if(Physics.Raycast(ray, out RaycastHit hit))
            {
                aimGroundLocation = hit.point;
            }
        }
        elapsedFireRateTime -= Time.deltaTime;
    }
}
