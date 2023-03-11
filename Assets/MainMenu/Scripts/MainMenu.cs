using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenu : MonoBehaviour
{
    public GameObject optionsMenu; //Object which is the parent to options menu content
    public GameObject mainMenuObj; //Object which is the parent to main menu content
    public GameObject controlScheme; //Object which contains the control scheme information
    public Animator fadeEffect;    //For transitioning from the main menu into the level hub

    private void Start()
    {
        //Ensures that when the player first enters the game, the main menu loads in correctly
        SoundManager.InitialiseMusicPlayer();
        SoundManager.PlayMusic(SoundManager.Music.Menu);
        optionsMenu.SetActive(false);
        controlScheme.SetActive(false);
        mainMenuObj.SetActive(true);
    }

    public void Controls()
    {
        SoundManager.PlaySound(SoundManager.Sound.MenuOptionSelected, gameObject);

        mainMenuObj.SetActive(!mainMenuObj.activeInHierarchy);
        controlScheme.SetActive(!controlScheme.activeInHierarchy);
    }

    public void Options()
    {
        SoundManager.PlaySound(SoundManager.Sound.MenuOptionSelected, gameObject);
       
        mainMenuObj.SetActive(!mainMenuObj.activeInHierarchy);
        optionsMenu.SetActive(!optionsMenu.activeInHierarchy);
    }

    public void PlayGame()
    {
        SoundManager.PlaySound(SoundManager.Sound.MenuOptionSelected, gameObject);

        SceneManager.LoadScene("LevelHub");

        LoadGame();
    }

    IEnumerator LoadGame()
    {
        fadeEffect.SetTrigger("Start");

        yield return new WaitForSeconds(Portal.maxFadeTime);

        SceneManager.LoadScene("LevelHub");

    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
