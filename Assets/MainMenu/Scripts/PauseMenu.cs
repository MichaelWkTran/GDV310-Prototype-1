using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public static bool isPaused = false; // Is the game paused
    public GameObject pauseMenu;
    public GameObject controlScheme;
    public GameObject optionsSelect;

    private void Start()
    {
        if (pauseMenu = GameObject.Find("PauseMenu"))
        {
            pauseMenu.SetActive(false);
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && pauseMenu != null)
        {
            isPaused = !isPaused;
            PauseGame();
        }
    }

    void PauseGame()
    {
        if(isPaused)
        {
            SoundManager.StopAllSounds();
            Time.timeScale = 0f;
            pauseMenu.SetActive(true);
        }
        else
        {
            Time.timeScale = 1f;
            pauseMenu.SetActive(false);
        }
    }

    public void QuitSession()
    {
        Application.Quit();
    }
    
    public void ContinuePlay()
    {
        isPaused = !isPaused;
        PauseGame();
    }

    public void ControlCheck()
    {

    }

    public void Options()
    {

    }

}
