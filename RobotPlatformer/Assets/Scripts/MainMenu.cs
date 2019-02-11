using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {


    public void StartGame()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        LevelChangerSc.Instance.FadeToLevel(SceneManager.GetActiveScene().buildIndex + 1);
        MusicChangerSc.Instance.FadeToLevel();
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
