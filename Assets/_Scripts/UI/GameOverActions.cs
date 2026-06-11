using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverActions : MonoBehaviour
{
    public void LoadScene(string sceneName)
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Chiudo il gioco");
    }
}
