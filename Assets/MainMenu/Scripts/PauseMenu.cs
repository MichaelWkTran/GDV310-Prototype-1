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
    public MouseLook cursorControl;

    /// <summary>
    /// Ensures the options selected and the pause menu are connected - Should mean that any level should pause
    /// </summary>
    private void Start()
    {
        if (pauseMenu = GameObject.Find("PauseSelection"))
        {
            pauseMenu.SetActive(false);
        }
        if (optionsSelect = GameObject.Find("OptionsMenu"))
        {
            optionsSelect.SetActive(false);
        }
        if (controlScheme = GameObject.Find("ControlScheme"))
        {
            controlScheme.SetActive(false);
        }
        cursorControl = GameObject.Find("Main Camera").GetComponent<MouseLook>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && pauseMenu != null)
        {
            isPaused = !isPaused;
            PauseGame();
        }
    }

    /// <summary>
    /// Handles the swapping in and out of paused and unpaused modes
    /// </summary>
    void PauseGame()
    {
        if(isPaused)
        {
            AudioListener.pause = true;
            Time.timeScale = 0f;
            pauseMenu.SetActive(true);
            cursorControl.m_CursorLocked = false;
            cursorControl.LockCursor();
        }
        else
        {
            AudioListener.pause = false;
            Time.timeScale = 1f;
            pauseMenu.SetActive(false);
            optionsSelect.SetActive(false);
            controlScheme.SetActive(false);
            cursorControl.m_CursorLocked = true;
            cursorControl.LockCursor();
            CurrentSoundUpdate();
        }
    }


    public void QuitSession()
    {
        Application.Quit();
    }
    
    /// <summary>
    /// For the continue play button - when the player uses the continue button it should 
    /// always make the pause state turn off and this way if the pause menu comes up unexpectedly
    /// it shouldn't break the game 
    /// </summary>
    public void ContinuePlay()
    {
        isPaused = false;
        PauseGame();
    }

    public void ControlCheck()
    {
        pauseMenu.SetActive(controlScheme.activeInHierarchy);
        controlScheme.SetActive(!controlScheme.activeInHierarchy);
    }

    public void Options()
    {
        pauseMenu.SetActive(optionsSelect.activeInHierarchy);
        optionsSelect.SetActive(!optionsSelect.activeInHierarchy);
        CurrentSoundUpdate();
    }

    public void CurrentSoundUpdate()
    {
        if (SoundManager.volumeUpdated)
        {
            //AudioSource[] audioSources = Object.FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
            //foreach (obj in audioSources)
            //{

            //}

            SoundManager.volumeUpdated = false;
        }
    }
}
