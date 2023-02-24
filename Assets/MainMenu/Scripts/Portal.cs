using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public int LevelNum;
    public bool StarterDoor;
    bool LockedDoor = false;

    private void LockChangeDoor()
    {
        LockedDoor = !LockedDoor;
    }

    private void Start()
    {
        if (!StarterDoor)
        {
            LevelNum = Random.Range(0, 3);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            print(LevelNum);
        }
    }
}
