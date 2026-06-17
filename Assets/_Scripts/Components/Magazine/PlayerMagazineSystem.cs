using System;
using UnityEngine;



/// <summary>
/// class to manage the player magazine.
/// </summary>

public class PlayerMagazineSystem : MagazineSystem
{
    public static event Action<int, BulletType> OnInfuseBullet;
    public static event Action<int> OnShoot;

    private int shotBulletCount;

    [Header("TEST")]
    [SerializeField] bool test;
    [SerializeField] private BulletType[] chamberTypes = new BulletType[6];

    public BulletType[] ChamberTypes => chamberTypes;

    private float currentDamageMultiplier = 1.0f;

    private void OnEnable()
    {
        PlayerController.OnReloadEvent += Reload;
    }
    private void OnDisable()
    {
        PlayerController.OnReloadEvent -= Reload;
    }

    public void UpgradeDamageMultiplier(float percentage)
    {
        currentDamageMultiplier += percentage;
    }

    private void Start()
    {
        if(test)
        {
            for (int i = 0; i < chamberTypes.Length; i++)
            {
                InfuseChamber(i, chamberTypes[i]);
            }

            return;
        }

        for (int i = 0; i < chamberTypes.Length; i++)
        {
            chamberTypes[i] = BulletType.Normal;
            InfuseChamber(i, chamberTypes[i]);
        }

        //// UNCOMMENT WHEN NOT TESTING
        ////for (int i = 0; i < chamberTypes.Length; i++)
        ////{
        ////    chamberTypes[i] = BulletType.Normal;
        ////}

        //// TEST ONLY
        //for(int i = 0; i < chamberTypes.Length; i++)
        //{
        //    if (i % 4 == 0)
        //    {
        //        InfuseChamber(i, BulletType.Fire);
        //    }
        //    else if(i % 2 == 0)
        //    {
        //        InfuseChamber(i, BulletType.Ice);
        //    }
        //    else
        //    {
        //        chamberTypes[i] = BulletType.Normal;
        //    }
        //}
    }

    public override BulletBehavior GetBullet()
    {
        
        if (shotBulletCount >= maxChambers) return null;

        BulletBehavior bullet = bulletPool.Get();  // Can't call base.GetBullet() because it will update the selectedChamber before we do the operations

        //Debug.Log($"Chamber type: {chamberTypes[shotBulletCount]}");

        BulletBehavior bulletBehavior = bullet.GetComponent<BulletBehavior>();
        bulletBehavior.DamageMultiplayer = currentDamageMultiplier;
        switch (chamberTypes[selectedChamber])
        {
            case BulletType.Fire:
                //CheckOrAddModifier(bullet, typeof(FireModifier));
                break;
            case BulletType.Ice:
                CheckOrAddModifier(bullet, typeof(IceModifier));
                break;
            default:
            case BulletType.Normal:

                break;
        }
        
        //if (chamberTypes[selectedChamber] != BulletType.Normal)
        //    Debug.Log($"Sto ritornando un proiettile magicoh: {chamberTypes[selectedChamber]}");

        OnShoot?.Invoke(selectedChamber);

        ChangeChamber();

        ++shotBulletCount;

        return bullet;
    }

    private void CheckOrAddModifier(BulletBehavior bullet, Type modifierType)
    {
        if (bullet.TryGetComponent(modifierType, out Component modifier))
        {
            if(modifier is Behaviour behaviour)
            {
                Debug.Log($"Enabled {modifierType}");
                behaviour.enabled = true;
            }
        }
        else
        {
            Debug.Log($"Added {modifierType}");
            Component component = bullet.gameObject.AddComponent(modifierType);
            if (component is Behaviour behaviour)
            {
                behaviour.enabled = true;
            }
        }
    }

    public void Reload()
    {
        shotBulletCount = 0;
    }

    public void InfuseChamber(int chamberNum, BulletType type)
    {
        if (chamberNum > chamberTypes.Length - 1 /*|| chamberTypes[chamberNum] == type*/) return;

        // ADD COMPONENTS TO BULLET

        chamberTypes[chamberNum] = type;

        OnInfuseBullet?.Invoke(chamberNum, type);
    }
}
