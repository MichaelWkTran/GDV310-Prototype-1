using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitDoor : Portal
{
    float yPos;
    bool win = false;
    public GameObject bars;
    public float barFallSpeed = 1.0f;

    // Start is called before the first frame update
    public override void Start()
    {
        yPos = bars.transform.position.y;
        win = false;
        base.Start();
        bars.transform.position = new Vector3(bars.transform.position.x, bars.transform.position.y + 100.0f, bars.transform.position.z);
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
            if (bars.transform.position.y > yPos)
            {
                float fallPos = bars.transform.position.y;
                fallPos -= Time.deltaTime * barFallSpeed;
                bars.transform.position = new Vector3(bars.transform.position.x, fallPos, bars.transform.position.z);
            }
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
        print(MusicCheck.enemyCount);
        if (other.tag == "MainCamera" && win)
        {
            pushbackText.SetActive(false);
            // teleport player back to hub/win place
            congratulationsText.SetActive(true);
            base.LoadLevel();
        }
        else
        {
            pushbackText.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        pushbackText.SetActive(false);
    }
}
