using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverActions : MonoBehaviour
{
    public void LoadScene(string sceneName)
    {
        Time.timeScale = 1.0f;
        StartCoroutine(PlaySoundAndLoadScene(sceneName));
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Chiudo il gioco");
    }

    private IEnumerator PlaySoundAndLoadScene(string sceneName)
    {
        AudioManager.PlaySound2D(SoundType.SelectionButton);

        yield return new WaitForSecondsRealtime(0.2f);

        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }
}
