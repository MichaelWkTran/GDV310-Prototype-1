using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenu : MonoBehaviour
{
    private void Start()
    {
    
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }
   
    public void QuitGame()
    {
        Application.Quit();
    }
}
