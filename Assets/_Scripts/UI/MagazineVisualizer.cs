using UnityEngine;
using UnityEngine.UI;

struct Chamber
{
    private BulletType type;
    private bool isShot;
    private int currentPosition;

    public BulletType Type { get { return type; } set { type = value; } }
    public bool IsShot { get { return isShot; } set { isShot = value; } }
    public int CurrentPosition { get { return currentPosition; } set { currentPosition = value; } }

    public Chamber(BulletType type = BulletType.Normal, bool isShot = false, int currentPosition = 0)
    {
        this.type = type;
        this.isShot = isShot;
        this.currentPosition = currentPosition;
    }
}

public class MagazineVisualizer : MonoBehaviour
{
    [SerializeField]
    GameObject[] chamberSprites;

    [SerializeField]
    private Color fireColor = Color.orange;
    [SerializeField]
    private Color iceColor = new Color(.2f, .64f, 1f);
    [SerializeField]
    private Color shotColor = Color.gray;

    private Chamber[] chambers;
    private int currentChamber;

    private void OnEnable()
    {
        chambers = new Chamber[chamberSprites.Length];

        for(int i = 0; i < chamberSprites.Length; i++)
        {
            chambers[i] = new Chamber(BulletType.Normal, false, i); // Init the chambers
        }

        PlayerMagazineSystem.OnInfuseBullet += UpdateChamber;
        PlayerMagazineSystem.OnShoot += RotateChamber;
        PlayerController.OnReloadEvent += Reload;
    }

    private void OnDisable()
    {
        PlayerMagazineSystem.OnInfuseBullet -= UpdateChamber;
        PlayerMagazineSystem.OnShoot -= RotateChamber;
        PlayerController.OnReloadEvent -= Reload;
    }

    private void UpdateChamber(int chamber, BulletType newChamberType)
    {
        chambers[chamber].Type = newChamberType;
        UpdateUI();
    }

    private void Reload()
    {
        for(int i = 0; i < chambers.Length; i++)
        {
            chambers[i].IsShot = false;
        }

        UpdateUI();
    }

    private void UpdateUI()
    {
        for(int i = 0; i < chambers.Length; i++)
        {
            if (chambers[i].IsShot)
            {
                chamberSprites[chambers[i].CurrentPosition].GetComponent<Image>().color = GetBulletColor(BulletType.Shot);
                continue;
            }

            chamberSprites[chambers[i].CurrentPosition].GetComponent<Image>().color = GetBulletColor(chambers[i].Type);
        }
    }

    private void RotateChamber(int shotChamber)
    {
        if (currentChamber >= chambers.Length) currentChamber = 0;

        chambers[shotChamber].IsShot = true;

        for (int i = 0; i < chambers.Length; i++)
        {
            --chambers[i].CurrentPosition;

            if (chambers[i].CurrentPosition < 0)
            {
                chambers[i].CurrentPosition = chambers.Length - 1;
            }
        }

        UpdateUI();
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
