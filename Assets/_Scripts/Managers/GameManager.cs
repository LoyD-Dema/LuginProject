using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("UI")]
    [SerializeField] private GameObject gameOverCanvas;
    [SerializeField] private TMP_Text timerText;

    private float elapsedTime = 0f;

    private void Awake()
    {
        if (Instance !=  null && Instance != this)
        {
            Destroy(gameObject);
        }
        Instance = this;
    }

    private void OnEnable()
    {
        HealthComponent.Death += OnActorDeath;
    }

    private void OnDisable()
    {
        HealthComponent.Death -= OnActorDeath;
    }

    private void Start()
    {
        gameOverCanvas.SetActive(false);

    }

    private void Update()
    {
        elapsedTime += Time.deltaTime;
        UpdateTimerUI();
    }

    private void UpdateTimerUI()
    {
        int totalSecond = Mathf.FloorToInt(elapsedTime);

        int hours = totalSecond / 3600;
        int minutes = (totalSecond % 3600) / 60;
        int seconds = totalSecond % 60;

        timerText.text = string.Format("{0:D2}:{1:D2}:{2:D2}", hours, minutes, seconds);

    }


    #region GameOver
    private void OnActorDeath(GameObject obj)
    {
        if (obj.CompareTag("Player"))
        {
            if (obj.TryGetComponent<PlayerController>(out var controller)) controller.enabled = false;
            if (obj.TryGetComponent<MovementComponent>(out var movement)) movement.enabled = false;
            if (obj.TryGetComponent<RotateToMouse>(out var rotator)) rotator.enabled = false;
            StartCoroutine(DelayedGameOver(3.2f));
            //TriggerGameOver();
        }
    }

    private System.Collections.IEnumerator DelayedGameOver(float delay)
    {
        yield return new WaitForSecondsRealtime(delay);

        TriggerGameOver();
    }

    private void TriggerGameOver()
    {
        Time.timeScale = 0f;

        if (gameOverCanvas != null)
        {
            gameOverCanvas.SetActive(true);
        }
    }
    #endregion
}
