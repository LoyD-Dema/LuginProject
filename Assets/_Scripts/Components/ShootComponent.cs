using System;
using UnityEngine;

[RequireComponent(typeof(MagazineSystem))]
public class ShootComponent : MonoBehaviour
{
    [Range(0.1f, 5.0f)]
    [SerializeField] float fireRate = 0.5f;
    [Range(0f, 5.0f)]
    [SerializeField] float distanceMultiplayer;
    private float elapsedFireRateTime;
    [SerializeField]
    private PlayerMagazineSystem magazine;

    // TODO - Change with the BulletBehavior Class
    //[SerializeField] GameObject bullet;

    private void Start()
    {
        elapsedFireRateTime = 0;
    }

    public void Shoot()
    {
        if (elapsedFireRateTime >= 0) return;

        // TODO - Change it by taking the bullet from the pool
        GameObject newBullet = magazine.GetBullet();
        if (!newBullet) return;
        newBullet.transform.position = transform.position + transform.forward * distanceMultiplayer;
        newBullet.transform.rotation = transform.rotation;
 
        elapsedFireRateTime = fireRate;
    }

    public void Reload()
    {
        magazine.Reaload();
    }

    private void Update()
    {
        elapsedFireRateTime -= Time.deltaTime;
    }
}
