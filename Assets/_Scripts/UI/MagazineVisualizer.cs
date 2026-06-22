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

    //[SerializeField]
    //private Color fireColor = Color.orange;
    //[SerializeField]
    //private Color iceColor = new Color(.2f, .64f, 1f);
    //[SerializeField]
    //private Color shotColor = Color.gray;

    [Header("Sprites")]
    [SerializeField] Sprite normalSprite;
    [SerializeField] Sprite fireSprite;
    [SerializeField] Sprite iceSprite;
    [SerializeField] Sprite shootSprite;
    [Header("Params")]
    [SerializeField] GameObject bulletsContainer;
    [Range(0.0f, 360.0f)]
    [SerializeField] float rotationAngle = 60.0f;
    [SerializeField] bool rotateToLeft;
    [SerializeField] float rotationSpeed = 10.0f;

    private Quaternion rotationToReach;
    private bool canRotateChamber;

    private Chamber[] chambers;
    private int currentChamber;

    private void OnEnable()
    {
        chambers = new Chamber[chamberSprites.Length];

        for (int i = 0; i < chamberSprites.Length; i++)
        {
            chambers[i] = new Chamber(BulletType.Normal, false, i); // Init the chambers
        }

        PlayerMagazineSystem.OnInfuseBullet += UpdateChamber;
        PlayerMagazineSystem.OnShoot += RotateChamber;
        PlayerController.OnReloadEvent += Reload;

        rotationAngle = rotateToLeft ? -rotationAngle : rotationAngle;
    }

    private void OnDisable()
    {
        PlayerMagazineSystem.OnInfuseBullet -= UpdateChamber;
        PlayerMagazineSystem.OnShoot -= RotateChamber;
        PlayerController.OnReloadEvent -= Reload;
    }

    private void Update()
    {
        if (!canRotateChamber)
            return;

        bulletsContainer.transform.rotation = Quaternion.Lerp(bulletsContainer.transform.rotation, rotationToReach, rotationSpeed * Time.deltaTime);
        if(Quaternion.Angle(bulletsContainer.transform.rotation, rotationToReach) < Quaternion.kEpsilon)
        {
            bulletsContainer.transform.rotation = rotationToReach;
            canRotateChamber = false;
        }

    }

    private void UpdateChamber(int chamber, BulletType newChamberType)
    {
        chambers[chamber].Type = newChamberType;
        UpdateUI();
    }

    private void Reload()
    {
        for (int i = 0; i < chambers.Length; i++)
        {
            chambers[i].IsShot = false;
        }

        UpdateUI();
    }

    private void UpdateUI()
    {
        for (int i = 0; i < chambers.Length; i++)
        {
            if (chambers[i].IsShot)
            {
                chamberSprites[chambers[i].CurrentPosition].GetComponent<Image>().sprite = GetBulletSprite(BulletType.Shot);
                continue;
            }

            chamberSprites[chambers[i].CurrentPosition].GetComponent<Image>().sprite = GetBulletSprite(chambers[i].Type);
        }
    }

    private void RotateChamber(int shotChamber)
    {
        if (currentChamber >= chambers.Length) currentChamber = 0;

        chambers[shotChamber].IsShot = true;

        //for (int i = 0; i < chambers.Length; i++)
        //{
        //    --chambers[i].CurrentPosition;

        //    if (chambers[i].CurrentPosition < 0)
        //    {
        //        chambers[i].CurrentPosition = chambers.Length - 1;
        //    }
        //}

        canRotateChamber = true;
        Vector3 eulerAngles = new Vector3(bulletsContainer.transform.rotation.eulerAngles.x, bulletsContainer.transform.rotation.eulerAngles.y, bulletsContainer.transform.rotation.eulerAngles.z + rotationAngle);
        rotationToReach = Quaternion.Euler(eulerAngles);


        UpdateUI();
    }

    private Sprite GetBulletSprite(BulletType chamberType)
    {
        switch (chamberType)
        {
            case BulletType.Normal:
                return normalSprite;
            case BulletType.Fire:
                return fireSprite;
            case BulletType.Ice:
                return iceSprite;
            case BulletType.Shot:
            default:
                return shootSprite;
        }
    }

    public void ForceSyncForCanvas(PlayerMagazineSystem magazine)
    {
        chambers = new Chamber[chamberSprites.Length];
        for (int i = 0; i < chambers.Length; i++)
        {
            chambers[i] = new Chamber(BulletType.Normal, false, i);
        }

        BulletType[] realBullets = magazine.ChamberTypes;

        for (int i = 0; i < realBullets.Length; i++)
        {
            chambers[i].Type = realBullets[i];
            chambers[i].IsShot = false;
        }

        UpdateUI();
    }
}
