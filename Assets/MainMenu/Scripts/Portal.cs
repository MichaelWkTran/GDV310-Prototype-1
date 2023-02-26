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
    public int LevelNum;              // Value of the room which a door leads through
    public bool starterDoor;
    public bool lockedDoor = true;

    public Animator transition;
    public float transitionTime = 2.0f;

    IEnumerator LoadLevel(GameObject other)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        if (LevelNum >= 1)
        {
            SceneManager.LoadScene(LevelNum + 1); // +1 so it doesn't transport back to level hub or main menu
        }
        else if (LevelNum == 0) {
            SceneManager.LoadScene("LevelHub");
        }
        //If we decide to use one scene instead use this code 
        //transition.SetTrigger("EndWait");
        //other.gameObject.GetComponent<CharacterController>().enabled = false;
        //other.transform.position = GameObject.Find("Starting Spot " + LevelNum).transform.position;
        //other.gameObject.GetComponent<CharacterController>().enabled = true;
    }

    private void LockChangeDoor()
    {
        lockedDoor = !lockedDoor;
    }

    private void Start()
    {
        if (!starterDoor && levelsExplored % 5 < 3)
        {
           // LevelNum = Random.Range(minRandomLevel, maxRandomLevel);
        }
        //else if (!starterDoor)
        //{
        ////    LevelNum = 6;
        //}
        transition = GameObject.Find("Image").GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !lockedDoor)
        {
            //Teleport in the same level
            //print("Starting Spot " + LevelNum);
            //other.gameObject.GetComponent<CharacterController>().enabled = false;
            //other.transform.position = GameObject.Find("Starting Spot " + LevelNum).transform.position;
            //other.gameObject.GetComponent<CharacterController>().enabled = true;

            //print(LevelsExplored);
            //LevelsExplored++;
            //print(LevelsExplored);

            StartCoroutine(LoadLevel(other.gameObject));
        }
    }
}