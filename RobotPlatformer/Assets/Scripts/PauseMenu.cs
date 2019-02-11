using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {
    [SerializeField]
    private GameObject pauseUIelement;
    [SerializeField]
    private GameObject gameSoundTrack;
    [SerializeField]
    private GameObject pauseSoundTrack;
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape) && Time.timeScale==1)
        {
            PauseGame();
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && Time.timeScale ==0)
        {
            ResumeGame();
        }
	}



    public void ResumeGame()
    {
        pauseUIelement.SetActive(false);
        Time.timeScale = 1;
        gameSoundTrack.SetActive(true);
        pauseSoundTrack.SetActive(false);
    }
    void PauseGame()
    {
        pauseUIelement.SetActive(true);
        Time.timeScale = 0;
        gameSoundTrack.SetActive(false);
        pauseSoundTrack.SetActive(true);
    }
    public void MainMenu()
    {
        LevelChangerSc.Instance.FadeToLevel(0);
        //MusicChangerSc.Instance.FadeToLevel();
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex-1);
        Time.timeScale = 1;
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
