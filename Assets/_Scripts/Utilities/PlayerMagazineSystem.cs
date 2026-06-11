using System;
using UnityEngine;

public enum BulletType
{
    Normal,
    Fire,
    Ice,
    Shot,
    LAST
}

public class PlayerMagazineSystem : MagazineSystem
{
    [SerializeField] // Remove serialize field (just for test)
    private BulletType[] chamberTypes = new BulletType[6];

    public static event Action<int, BulletType> OnInfuseBullet;
    public static event Action<int> OnShoot;

    private int shotBulletCount;

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
        // UNCOMMENT WHEN NOT TESTING
        //for (int i = 0; i < chamberTypes.Length; i++)
        //{
        //    chamberTypes[i] = BulletType.Normal;
        //}

        // TEST ONLY
        for(int i = 0; i < chamberTypes.Length; i++)
        {
            if (i % 4 == 0)
            {
                InfuseChamber(i, BulletType.Fire);
            }
            else if(i % 2 == 0)
            {
                InfuseChamber(i, BulletType.Ice);
            }
            else
            {
                chamberTypes[i] = BulletType.Normal;
            }
        }
    }

    public override GameObject GetBullet()
    {
        if (shotBulletCount >= maxChambers) return null;

        GameObject bullet = Instantiate(bulletPrefab); // Can't call base.GetBullet() because it will update the selectedChamber before we do the operations

        if (chamberTypes[selectedChamber] != BulletType.Normal)
        {
            Debug.Log($"Sto ritornando un proiettile magicoh: {chamberTypes[selectedChamber]}");
        }

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
        if (chamberNum > chamberTypes.Length - 1 || chamberTypes[chamberNum] == type) return;

        // ADD COMPONENTS TO BULLET

        chamberTypes[chamberNum] = type;

        OnInfuseBullet?.Invoke(chamberNum, type);
    }
}
