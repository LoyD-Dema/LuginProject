using System;
using UnityEngine;
using UnityEngine.InputSystem;

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

    [SerializeField] GameObject bullet;
    [SerializeField] Transform bulletSpawn;

    public void Shoot()
    {
        if (elapsedFireRateTime >= 0) return;

        GameObject newBullet = magazine.GetBullet();
        if (!newBullet) return;
        newBullet.transform.position = bulletSpawn;
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
