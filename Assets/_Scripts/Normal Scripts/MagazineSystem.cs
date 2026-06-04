using UnityEngine;

public enum BulletType
{
    Normal,
    Fire,
    Ice,
    LAST
}

public class MagazineSystem : MonoBehaviour
{
    [SerializeField] // Remove serialize field (just for test)
    private BulletType[] chamberTypes = new BulletType[6];
    [SerializeField]
    private GameObject bulletPrefab;
    [SerializeField]
    private const ushort maxChambers = 6;

    private ushort selectedChamber = 0;

    // UNCOMMENT WHEN NOT TESTING
    //private void Start()
    //{
    //    for(int i = 0; i < chamberTypes.Length; i++)
    //    {
    //        chamberTypes[i] = BulletType.Normal;
    //    }
    //}

    public GameObject GetBullet()
    {
        if (chamberTypes[selectedChamber] != BulletType.Normal)
        {
            Debug.Log($"Sto ritornando un proiettile magicoh: {chamberTypes[selectedChamber]}");
        }

        GameObject bullet = Instantiate(bulletPrefab); // CHANGE WITH BULLET POOL

        ChangeChamber();

        return bullet;
    }

    private void ChangeChamber()
    {
        ++selectedChamber;

        if(selectedChamber >= maxChambers)
        {
            selectedChamber = 0;
        }
    }

    public void InfuseChamber(ushort chamberNum, BulletType type)
    {
        if (chamberNum > chamberTypes.Length - 1) return;

        // ADD COMPONENT TO BULLET

        chamberTypes[chamberNum] = type;
    }
}
