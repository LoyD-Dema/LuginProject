using System;
using UnityEngine;
using UnityEngine.UI;

public class MagazineVisualizer : MonoBehaviour
{
    [SerializeField]
    GameObject[] chambers;

    private BulletType[] chamberTypes;
    private ushort currentChamber;

    private void Start()
    {
        chamberTypes = new BulletType[chambers.Length];

        PlayerMagazineSystem.OnInfuseBullet += UpdateUI;
        PlayerMagazineSystem.OnShoot += RotateChamber;
    }

    private void UpdateUI(ushort chamber, BulletType chamberType)
    {
        if (chamber < 0) return;

        chambers[chamber].GetComponent<Image>().color = GetBulletColor(chamberType);
        chamberTypes[chamber] = chamberType;
    }

    private void RotateChamber(ushort shotChamber)
    {
        for(int i = shotChamber; i < chambers.Length + shotChamber; i++)
        {
            ushort chamberToModify = Convert.ToUInt16(i % chambers.Length);
            BulletType bulletType = chamberTypes[i % chambers.Length];

            Debug.Log($"Chamber to modify: {chamberToModify}");
            Debug.Log($"Modification to: {bulletType}");

            UpdateUI(chamberToModify, bulletType);
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
