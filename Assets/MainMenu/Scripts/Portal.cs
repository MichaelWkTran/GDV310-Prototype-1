
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    public static int checkpointUnlocked; // Highest Checkpoint cleared
    const int maxLevelsExplored = 15; // The amount of rooms a player can explore until endgame *pressuming narrative based game
    //private int minRandomLevel = 1;   // Minimum level value
    //private int maxRandomLevel = 4;   // Maximum level value
    public int LevelNum;              // Value of the room which a portal leads through
    public bool starterPortal;        // Whether a portal is to/from the hub world
    public bool lockedPortal = true;  // If true, the player should not be able to pass through it

    //An
    public Animator fadeEffect;
    public static float maxFadeTime = 2.0f;
    public static float fadeTime;

    public Material lockedMat;
    public Material unlockedMat;
    public GameObject pushbackText;
    public GameObject congratulationsText;
    /// <summary>
    /// Loads
    /// </summary>
    /// <param name="other">The player game object, used for teleporting versions </param>
    /// <returns></returns>
    IEnumerator LoadLevel(bool preventAccess)
    {

        //Need to fade in and out music as well
        fadeEffect.SetTrigger("Start");

        if (preventAccess)
        {
            pushbackText.SetActive(true);
        }
        yield return new WaitForSeconds(maxFadeTime);

        if (preventAccess)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        if (LevelNum >= 1)
        {
            SceneManager.LoadScene(LevelNum + 1); // +1 so it doesn't transport back to level hub or main menu
        }

        else if (LevelNum == 0)
        {
            SceneManager.LoadScene("LevelHub");
        }

    }
    ///If we decide to use a teleport within one scene instead use this code, or remove the transition for a teleporter on the same map
    //transition.SetTrigger("EndWait");
    //other.gameObject.GetComponent<CharacterController>().enabled = false;
    //other.transform.position = GameObject.Find("Starting Spot " + LevelNum).transform.position;
    //other.gameObject.GetComponent<CharacterController>().enabled = true;

    /// <summary>
    /// Allows other scripts to change the locked state of the door
    /// Which will allow us to let players slowly unlock levels instead of having them all accessible at once
    /// </summary>
    public void LockChangeDoor()
    {
        lockedPortal = !lockedPortal;
    }

    public virtual void Start()
    {
        /// <summary>
        /// Randomiser code for the doors not starting the level
        /// Used if we decide to create a randomised room effect
        ///if (!starterDoor && levelsExplored % 5 < 3)
        ///{
        /// LevelNum = Random.Range(minRandomLevel, maxRandomLevel);
        ///}
        ///else if (!starterDoor)
        ///{
        ///    LevelNum = 6;
        ///}
        /// </summary>
        congratulationsText.SetActive(false);
        fadeEffect = GameObject.Find("Image").GetComponent<Animator>(); //Name of loading screen-> animatior fades it in and out between scenes
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();

        if (lockedPortal) //Sets the mesh to the corresponding mesh state
        {
            meshRenderer.material = lockedMat;
        }
        else
        {
            meshRenderer.material = unlockedMat;
        }

    }

    // Starts the process of moving the player into a new scene
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "MainCamera")
            if (!lockedPortal)
            {
                StartCoroutine(LoadLevel(false));
            }
            // Since there are portals the player can attempt to enter that lead to nowhere and there's little feedback, added in a transition back to the same scene
            // This only occurs if this is attempted on the initial screen
            else if (starterPortal)
            {
                StartCoroutine(LoadLevel(true));
            }
    }

    public void LoadLevel()
    {
        StartCoroutine(LoadLevel(false));
    }

    public static void LoadLevelHub()
    {
        fadeTime = maxFadeTime;

    }
    /// Levels explored would increment when a player reaches the end of a level and enters another one. Potentially used for difficulty adjustment and affecting room generation
    /// Reset when the player dies or returns to the hub
    /// print(LevelsExplored);
    /// LevelsExplored++;
    /// print(LevelsExplored);
}
