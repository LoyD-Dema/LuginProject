using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(MagazineSystem))]
public class ShootComponent : MonoBehaviour
{
    [Range(0.1f, 5.0f)]
    [SerializeField] float fireRate = 0.5f;
    private float elapsedFireRateTime;
    
    [SerializeField]
    private MagazineSystem magazine;

    [SerializeField] Transform bulletSpawn;

    public void Shoot()
    {
        if (elapsedFireRateTime >= 0)
            return;

        BulletBehavior newBullet = magazine.GetBullet();
        
        if (!newBullet) 
            return;

        AudioManager.PlaySound3D(SoundType.CowboyShoot, transform.position,2);
        newBullet.transform.position = bulletSpawn.position;
        newBullet.transform.rotation = transform.rotation;
        elapsedFireRateTime = fireRate;
    }

    private void Update()
    {
        elapsedFireRateTime -= Time.deltaTime;
    }
}
