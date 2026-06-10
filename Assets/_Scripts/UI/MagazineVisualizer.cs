using UnityEngine;
using UnityEngine.UI;

public class MagazineVisualizer : MonoBehaviour
{
    [SerializeField]
    GameObject[] chambers;

    [SerializeField]
    private Color fireColor = Color.orange;
    [SerializeField]
    private Color iceColor = new Color(.2f, .64f, 1f);
    [SerializeField]
    private Color shotColor = Color.gray;

    private BulletType[] chamberTypes;
    private BulletType[] currentChamberTypes;
    private int currentChamber;

    private void OnEnable()
    {
        chamberTypes = new BulletType[chambers.Length];
        currentChamberTypes = new BulletType[chambers.Length];

        PlayerMagazineSystem.OnInfuseBullet += UpdateChamber;
        PlayerMagazineSystem.OnShoot += RotateChamber;
        PlayerMagazineSystem.OnReload += InitChamber;
    }

    private void OnDisable()
    {
        PlayerMagazineSystem.OnInfuseBullet -= UpdateChamber;
        PlayerMagazineSystem.OnShoot -= RotateChamber;
        PlayerMagazineSystem.OnReload -= InitChamber;
    }

    private void Start()
    {
        InitChamber();
    }

    private void InitChamber()
    {
        currentChamber = 0;

        for (int i = 0; i < chamberTypes.Length; i++)
        {
            UpdateUI(i, chamberTypes[i]);
        }
    }

    private void UpdateChamber(int chamber, BulletType newChamberType)
    {
        chamberTypes[chamber] = newChamberType;
    }

    private void UpdateUI(int chamber, BulletType chamberType)
    {
        if (chamber < 0 || chamber >= chambers.Length) return;

        chambers[chamber].GetComponent<Image>().color = GetBulletColor(chamberType);
        currentChamberTypes[chamber] = chamberType;
    }

    private void RotateChamber(int shotChamber)
    {
        if (currentChamber >= chambers.Length) currentChamber = 0;

        currentChamber = shotChamber + 1;

        for (int i = 0; i < chambers.Length; i++)
        {
            int chamberIndex = (currentChamber + i) % chambers.Length;

            if (chambers.Length - i <= currentChamber)
            {
                UpdateUI(i, BulletType.Shot);
            }
            else
            {
                UpdateUI(i, chamberTypes[chamberIndex]);
            }
        }
    }

    private Color GetBulletColor(BulletType chamberType)
    {
        switch (chamberType)
        {
            case BulletType.Normal:
                return Color.white;
            case BulletType.Fire:
                return fireColor;
            case BulletType.Ice:
                return iceColor;
            case BulletType.Shot:
                return shotColor;
            default:
                return Color.red;
        }
    }
}
