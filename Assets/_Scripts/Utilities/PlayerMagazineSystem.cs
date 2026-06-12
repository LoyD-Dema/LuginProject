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

    private void OnEnable()
    {
        PlayerController.OnReloadEvent += Reload;
    }
    private void OnDisable()
    {
        PlayerController.OnReloadEvent -= Reload;
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

    public override GameObject GetBullet()
    {
        
        if (shotBulletCount >= maxChambers) return null;

        GameObject bullet = Instantiate(bulletPrefab); // Can't call base.GetBullet() because it will update the selectedChamber before we do the operations

        Debug.Log($"Chamber type: {chamberTypes[shotBulletCount]}");
        
        switch (chamberTypes[selectedChamber])
        {
            case BulletType.Fire:
                break;
            case BulletType.Ice:
                bullet.AddComponent<IceModifier>();
                Debug.Log("Shot Ice bullet");
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
