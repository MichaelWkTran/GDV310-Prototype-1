using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    public static int levelsExplored; // Amount of rooms explored in a run 
    const int maxLevelsExplored = 15; // The amount of rooms a player can explore until endgame *pressuming narrative based game
    private int minRandomLevel = 1;   // Minimum level value 
    private int maxRandomLevel = 4;   // Maximum level value
    public int LevelNum;              // Value of the room which a portal leads through
    public bool starterPortal;        // Whether a portal is to/from the hub world  
    public bool lockedPortal = true;  // If true, the player should not be able to pass through it

    public Animator fadeEffect;
    public static float maxFadeTime = 2.0f;
    public static float fadeTime;     //  
    
    /// <summary>
    /// Loads 
    /// </summary>
    /// <param name="other">The player game object, used for teleporting versions </param>
    /// <returns></returns>
    IEnumerator LoadLevel(GameObject other)
    {

        //Need to fade in and out music as well


        fadeEffect.SetTrigger("Start");

        yield return new WaitForSeconds(fadeTime);

        if (LevelNum >= 1)
        {
            SceneManager.LoadScene(LevelNum + 1); // +1 so it doesn't transport back to level hub or main menu
        }
        else if (LevelNum == 0) {
            SceneManager.LoadScene("LevelHub");
        }
    }

    public static void LoadLevel()
    {
        print(fadeTime);
        //yield return new WaitForSeconds(fadeTime);
  
        SceneManager.LoadScene(1);
        
    }
    ///If we decide to use a teleport within one scene instead use this code, or remove the transition for a teleporter on the same map 
    //transition.SetTrigger("EndWait");
    //other.gameObject.GetComponent<CharacterController>().enabled = false;
    //other.transform.position = GameObject.Find("Starting Spot " + LevelNum).transform.position;
    //other.gameObject.GetComponent<CharacterController>().enabled = true;

    /// <summary>
    /// Allows other scripts to change the locked state of the door
    /// Which will allow us to let players slowly unlock levels instead of having them all accessible at once
    /// 
    /// </summary>
    public void LockChangeDoor()
    {
        lockedPortal = !lockedPortal;
    }

    private void Start()
    {
        // Randomiser code for the doors not starting the level 
        // Used if we decide to create a randomised room effect
        //if (!starterDoor && levelsExplored % 5 < 3)
        //{
        // LevelNum = Random.Range(minRandomLevel, maxRandomLevel);
        //}
        //else if (!starterDoor)
        //{
        ////    LevelNum = 6;
        //}
        fadeTime = maxFadeTime;
        fadeEffect = GameObject.Find("Image").GetComponent<Animator>(); //Name of loading screen-> animatior fades it in and out between scenes
    }

    // Starts the process of moving the player into a new scene 
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !lockedPortal)
        { 
            StartCoroutine(LoadLevel(other.gameObject));
        }
    }

    /// Levels explored would increment when a player reaches the end of a level and enters another one. Potentially used for difficulty adjustment
    /// Reset when the player dies or returns to the hub
    // print(LevelsExplored);
    // LevelsExplored++;
    // print(LevelsExplored);
}