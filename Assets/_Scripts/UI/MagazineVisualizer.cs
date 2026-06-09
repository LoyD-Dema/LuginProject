using UnityEngine;
using UnityEngine.UI;

public class MagazineVisualizer : MonoBehaviour
{
    [SerializeField]
    GameObject[] chambers;

    private BulletType[] chamberTypes;
    private BulletType[] currentChamberTypes;
    private int currentChamber;

    private void OnEnable()
    {
        chamberTypes = new BulletType[chambers.Length];
        currentChamberTypes = new BulletType[chambers.Length];

        PlayerMagazineSystem.OnInfuseBullet += UpdateChamber;
        PlayerMagazineSystem.OnShoot += RotateChamber;
    }

    private void UpdateChamber(int chamber, BulletType newChamberType)
    {
        chamberTypes[chamber] = newChamberType;
    }

    private void UpdateUI(int chamber, BulletType chamberType)
    {
        if (chamber < 0) return;

        chambers[chamber].GetComponent<Image>().color = GetBulletColor(chamberType);
        currentChamberTypes[chamber] = chamberType;
    }

    private void RotateChamber(int shotChamber)
    {
        currentChamber = shotChamber + 1;

        for (int i = 0; i < chambers.Length; i++)
        {
            int chamberIndex = (currentChamber + i) % chambers.Length;
            UpdateUI(i, chamberTypes[chamberIndex]); 
        }
    }

    private Color GetBulletColor(BulletType chamberType)
    {
        switch (chamberType)
        {
            case BulletType.Normal:
                return Color.white;
            case BulletType.Fire:
                return Color.orange;
            case BulletType.Ice:
                return new Color(.2f, .64f, 1f);
            default:
                return Color.red;
        }
    }
}
