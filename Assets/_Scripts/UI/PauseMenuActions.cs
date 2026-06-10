using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuActions : MonoBehaviour
{
    public event Action OnResumeGame;
    //Usata sia per ricominciare la Run che per andare al menu principale
    public void LoadScene(string sceneName)
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }

    //Usata per riprendere la partita
    public void ResumeGame()
    {
        OnResumeGame?.Invoke();
    }
}
