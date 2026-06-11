using TMPro;
using UnityEngine;

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
    private void OnActorDeath(GameObject deadObject)
    {
        if (deadObject.CompareTag("Player"))
        {
            TriggerGameOver();
        }
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
