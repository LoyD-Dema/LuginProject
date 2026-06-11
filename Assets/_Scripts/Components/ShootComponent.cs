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

    [SerializeField] private Camera camera;
    private Vector3 aimLocation;

    private void Start()
    {
        elapsedFireRateTime = 0;
    }

    public void Shoot()
    {
        if (elapsedFireRateTime >= 0) return;

        // TODO - Change it by taking the bullet from the pool
        Vector3 bulletSpawnDirectionNormalized = (aimLocation - bulletSpawn.position).normalized;
        Quaternion rotation = Quaternion.LookRotation(aimLocation - bulletSpawn.position);
        
        Instantiate(bullet, bulletSpawn.position + bulletSpawnDirectionNormalized, rotation);
        elapsedFireRateTime = fireRate;
    }

    private void Update()
    {
        //Evaluate shooting direction depending on cursor position
        Ray ray = camera.ScreenPointToRay(Mouse.current.position.ReadValue());
        if(Physics.Raycast(ray, out RaycastHit hit))
        {
            aimLocation = hit.point;
        }
        
        elapsedFireRateTime -= Time.deltaTime;
    }
}
