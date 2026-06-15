using System;
using System.Collections;
using UnityEngine;


public class BaseModifier : MonoBehaviour
{
    protected float amountDamageMultiplier;
    protected float amountSpeedMultiplier;

    protected BulletBehavior bullet;

    protected virtual void Awake()
    {
        bullet = GetComponent<BulletBehavior>();
    }


    protected virtual void OnEnable()
    {
        bullet.OnInstantiate += Bullet_OnInstantiate;
        bullet.OnTraveling += Bullet_OnTraveling;
        bullet.OnHitTrigger += Bullet_OnHitTrigger;

    }

    protected virtual void OnDisable()
    {
        bullet.OnInstantiate -= Bullet_OnInstantiate;
        bullet.OnTraveling -= Bullet_OnTraveling;
        bullet.OnHitTrigger -= Bullet_OnHitTrigger;
    }

    protected virtual void Bullet_OnInstantiate(object sender, EventArgs e)
    {
        // Do the base logic of a modifer when the bullet in Instantiated (taken from the pool)
    }

    private void Bullet_OnTraveling(object sender, EventArgs e)
    {
        // Do the base logic of a modifer when the bullet in Traveling
    }

    protected virtual void Bullet_OnHitTrigger(object sender, OnHitEventArgs e)
    {
        // Do the base logic of a modifer when the bullet hit a trigger

        enabled = false;    
    }
}
