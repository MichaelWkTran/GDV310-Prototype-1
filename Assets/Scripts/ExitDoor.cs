using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitDoor : MonoBehaviour
{
    float yPos;
    public GameObject congratulationsText; //Text telling the player they cleared the game 


    // Start is called before the first frame update
    void Start()
    {
        yPos = transform.position.y; 

        transform.position = new Vector3(transform.position.x, -1000.0f, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.FindGameObjectsWithTag("Enemy").Length == 0) // all enemies defeated
        {
            transform.position = new Vector3(transform.position.x, yPos, transform.position.z);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Main Camera")
        {
            // teleport player back to hub/win place
            SceneManager.LoadScene(sceneBuildIndex:1);
        }
    }
}
