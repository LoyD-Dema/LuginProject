using UnityEngine;
using UnityEngine.SceneManagement;

/*
 Classe che va a definire le azioni che possono essere eseguite dai bottoni
 */
public class MainMenuActions : MonoBehaviour
{

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Chiudo il gioco");
    }
}
