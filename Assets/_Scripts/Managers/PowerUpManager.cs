using UnityEngine;

public class PowerUpManager : MonoBehaviour
{

    [SerializeField] private GameObject powerUpCanvas;
    [SerializeField] private LevelBar levelBar;

    private bool isPowerUpOpen = false;

    private void OnEnable()
    {
        if (levelBar != null) levelBar.OnLevelUp += DisplayPowerUpMenu;
    }

    private void OnDisable()
    {
        if (levelBar != null) levelBar.OnLevelUp -= DisplayPowerUpMenu;
    }

    private void DisplayPowerUpMenu()
    {
        isPowerUpOpen = !isPowerUpOpen;
        if (powerUpCanvas != null)
        {
            Time.timeScale = isPowerUpOpen ? 0 : 1.0f;
            powerUpCanvas.SetActive(isPowerUpOpen);
        }
    }


}
