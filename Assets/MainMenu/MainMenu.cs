using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenu : MonoBehaviour
{
    AudioSource QuitCheck;
    private void Start()
    {
        QuitCheck = GameObject.Find("MenuObject").GetComponent<AudioSource>();
    }

    public void PlayGame()
    {
        QuitCheck.Play();
        //SceneManager.LoadScene(1);
    }
    public void QuitGame()
    {
        //QuitCheck.Play();
        //Application.Quit();
    }
    //void 
}
