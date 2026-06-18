using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
 Classe che va a definire le azioni che possono essere eseguite dai bottoni
 */
public class MainMenuActions : MonoBehaviour
{

    public void LoadScene(string sceneName)
    {
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
