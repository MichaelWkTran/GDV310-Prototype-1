using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenu : MonoBehaviour
{
    public GameObject optionsMenu; //Object which is the parent to options menu content
    public GameObject mainMenuObj; //Object which is the parent to main menu content
    public Animator fadeEffect;    //For transitioning from the main menu into the level hub

    private void Start()
    {
        //Ensures that when the player first enters the game, the main menu loads in correctly
        SoundManager.InitialiseMusicPlayer();
        SoundManager.PlayMusic(SoundManager.Music.Menu);
        optionsMenu.SetActive(false);
        mainMenuObj.SetActive(true);
    }

    public void Options()
    {
        SoundManager.PlaySound(SoundManager.Sound.MenuOptionSelected, gameObject);
       
        mainMenuObj.SetActive(!mainMenuObj.activeInHierarchy);
        optionsMenu.SetActive(!optionsMenu.activeInHierarchy);
    }

    public void PlayGame()
    {
        fadeEffect.SetTrigger("Start");

        Portal.LoadLevel();
    }
   
    public void QuitGame()
    {
        Application.Quit();
    }
}
