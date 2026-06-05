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

    // UNCOMMENT WHEN NOT TESTING
    //private void Start()
    //{
    //    for(int i = 0; i < chamberTypes.Length; i++)
    //    {
    //        chamberTypes[i] = BulletType.Normal;
    //    }
    //}

    public override GameObject GetBullet()
    {
        GameObject bullet = base.GetBullet();

        if (chamberTypes[selectedChamber] != BulletType.Normal)
        {
            Debug.Log($"Sto ritornando un proiettile magicoh: {chamberTypes[selectedChamber]}");
        }

        return bullet;
    }

    public void InfuseChamber(ushort chamberNum, BulletType type)
    {
        if (chamberNum > chamberTypes.Length - 1) return;

        // ADD COMPONENT TO BULLET

        chamberTypes[chamberNum] = type;
    }
}
