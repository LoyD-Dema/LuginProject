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
    private MagazineSystem magazine;

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
        if (magazine.GetType() != typeof(PlayerMagazineSystem)) return; // Normal magazine system can't accidentally call this method

        PlayerMagazineSystem pm = (PlayerMagazineSystem)magazine;
        pm.Reload();
    }

    private void Update()
    {
        elapsedFireRateTime -= Time.deltaTime;
    }
}
