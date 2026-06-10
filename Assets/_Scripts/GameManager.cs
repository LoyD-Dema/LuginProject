using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("UI")]
    [SerializeField] private GameObject gameOverCanvas;

    private void OnEnable()
    {
        HealthComponent.Death += OnActorDeath;
    }

    private void OnDisable()
    {
        HealthComponent.Death -= OnActorDeath;
    }

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
}
