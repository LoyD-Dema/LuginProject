using System;
using UnityEngine;

public enum BulletType
{
    Normal,
    Fire,
    Ice,
    LAST
}

public class PlayerMagazineSystem : MagazineSystem
{
    [SerializeField] // Remove serialize field (just for test)
    private BulletType[] chamberTypes = new BulletType[6];

    public static Action<ushort, BulletType> OnInfuseBullet;
    public static Action<ushort> OnShoot;

    private void Start()
    {
        // UNCOMMENT WHEN NOT TESTING
        //for (int i = 0; i < chamberTypes.Length; i++)
        //{
        //    chamberTypes[i] = BulletType.Normal;
        //}

        // TEST ONLY
        for(ushort i = 0; i < chamberTypes.Length; i++)
        {
            if (chamberTypes[i] != BulletType.Normal)
            {
                InfuseChamber(i, chamberTypes[i]);
            }
        }
    }

    public override GameObject GetBullet()
    {
        GameObject bullet = Instantiate(bulletPrefab); // Can't call base.GetBullet() because it will update the selectedChamber before we do the operations

        if (chamberTypes[selectedChamber] != BulletType.Normal)
        {
            Debug.Log($"Sto ritornando un proiettile magicoh: {chamberTypes[selectedChamber]}");
        }

        OnShoot?.Invoke(selectedChamber);

        ChangeChamber();

        return bullet;
    }

    public void InfuseChamber(ushort chamberNum, BulletType type)
    {
        if (chamberNum > chamberTypes.Length - 1) return;

        // ADD COMPONENT TO BULLET

        chamberTypes[chamberNum] = type;

        OnInfuseBullet?.Invoke(chamberNum, type);
    }
}
