using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ChooseChamber : MonoBehaviour
{
    [Header("UI Refs")]
    [SerializeField] private RectTransform magazineRect;
    [SerializeField] private Image bulletImage;
    [SerializeField] private MagazineVisualizer magazineVisualizer;

    [Header("Config")]
    [SerializeField] private float rotationSpeed = 10f;

    private PlayerMagazineSystem playerMagazine;
    private PowerUpData currentBulletData;
    private PowerUpManager powerUpManager;
    private int currentChamberIndex = 0;
    private float targetRotation = 0;

    private void OnEnable()
    {
        ResetDrumToZero();
    }

    private void ResetDrumToZero()
    {
        currentChamberIndex = 0;
        targetRotation = 0;
        magazineRect.localRotation = Quaternion.identity;
    }

    public void SetupMagazineMenu(PowerUpData data, PlayerMagazineSystem magazine, PowerUpManager manager)
    {
        currentBulletData = data;
        playerMagazine = magazine;
        powerUpManager = manager;

        bulletImage.sprite = data.Icon;

        currentChamberIndex = 0;
        targetRotation = 0f;

        magazineRect.localRotation = Quaternion.identity;

        magazineVisualizer.ForceSyncForCanvas(playerMagazine);
    }

    public void Update()
    {
        Quaternion targetRot = Quaternion.Euler(0, 0, targetRotation);
        magazineRect.localRotation = Quaternion.Lerp(magazineRect.localRotation, targetRot, rotationSpeed * Time.unscaledDeltaTime);
    }

    private void RotateDrum(int direction)
    {
        targetRotation += direction * 60f;

        currentChamberIndex += direction;

        currentChamberIndex = (currentChamberIndex % 6 + 6) % 6;

    }

    public void SelectChamber()
    {
        if (playerMagazine == null || currentBulletData == null) return;

        playerMagazine.InfuseChamber(currentChamberIndex, currentBulletData.Modifier.bulletType);

        Debug.Log($"Proiettile {currentBulletData.Modifier.bulletType} inserito nella camera {currentChamberIndex}!");

        if (powerUpManager != null)
        {
            powerUpManager.OnChamberSelectionConfirmed();
        }
    }

    #region PlayerInput Helper
    public void MoveLeft()
    {
        RotateDrum(-1);
    }

    public void MoveRight()
    {
        RotateDrum(1);
    }
    #endregion
}
