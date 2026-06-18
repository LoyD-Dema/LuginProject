using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuActions : MonoBehaviour
{
    public event Action OnResumeGame;
    //Usata sia per ricominciare la Run che per andare al menu principale
    public void LoadScene(string sceneName)
    {
        Time.timeScale = 1.0f;
        StartCoroutine(PlaySoundAndLoadScene(sceneName));
    }

    //Usata per riprendere la partita
    public void ResumeGame()
    {
        AudioManager.PlaySound2D(SoundType.SelectionButton);
        OnResumeGame?.Invoke();
    }

    private IEnumerator PlaySoundAndLoadScene(string sceneName)
    {
        AudioManager.PlaySound2D(SoundType.SelectionButton);

        yield return new WaitForSecondsRealtime(0.2f);

        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }
}
