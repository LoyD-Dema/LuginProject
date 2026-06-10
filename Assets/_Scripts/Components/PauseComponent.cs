using UnityEngine;
using UnityEngine.InputSystem;

public class PauseComponent : MonoBehaviour
{
    [Header("Canvas Ref")]
    [SerializeField] private Canvas pauseMenuCanvas;
    //[SerializeField] private Canvas powerUpCanvas;

    private bool isPaused = false;
    //private bool isPowerUp = false;

    private PlayerController playerController;

    //private LevelBar levelBar;

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
        //levelBar = GetComponent<LevelBar>();
    }

    private void OnEnable()
    {
        playerController.OnPausePressed += TogglePauseMenu;
        pauseMenuCanvas.GetComponent<PauseMenuActions>().OnResumeGame += TogglePauseMenu; 
        //levelBar.OnLevelUp += TogglePowerUpMenu;
    }

    private void OnDisable()
    {
        playerController.OnPausePressed -= TogglePauseMenu;
        if (pauseMenuCanvas != null) pauseMenuCanvas.GetComponent<PauseMenuActions>().OnResumeGame -= TogglePauseMenu;
        //levelBar.OnLevelUp -= TogglePowerUpMenu;
    }

    private void TogglePauseMenu()
    {
        isPaused = !isPaused;
        playerController.IsPaused = isPaused;
        Time.timeScale = isPaused ? 0f : 1.0f;
        pauseMenuCanvas.gameObject.SetActive(isPaused);
    }

    //private void TogglePowerUpMenu()
    //{
    //    isPowerUp = !isPowerUp;
    //    Time.timeScale = isPowerUp ? 0f : 1.0f;
    //    powerUpCanvas.gameObject.SetActive(isPowerUp);
    //}

}
