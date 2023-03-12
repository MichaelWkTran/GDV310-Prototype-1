using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitDoor : Portal
{
    float yPos;
    bool win = false;

    // Start is called before the first frame update
    public override void Start()
    {
        yPos = transform.position.y;
        win = false;
        base.Start();
        // transform.position = new Vector3(transform.position.x, -1000.0f, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
       if (MusicCheck.enemyCount == 0) // all enemies defeated
       {
            //transform.position = new Vector3(transform.position.x, yPos, transform.position.z);
            lockedPortal = false;
            GetComponent<MeshRenderer>().material = unlockedMat;
            win = true;
       }
        else
        {
            lockedPortal = true;
            GetComponent<MeshRenderer>().material = lockedMat;
            win = false;
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Main Camera" && win)
        {
            // teleport player back to hub/win place
            congratulationsText.SetActive(true);
            base.LoadLevel();
        }
    }
}
