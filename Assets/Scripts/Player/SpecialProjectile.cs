using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialProjectile : MonoBehaviour
{


    // Start is called before the first frame update
    void Start()
    {
        SoundManager.Play3DSound(SoundManager.Sound.SpecialAttack, gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy") // enemy hit by attack
        {
            Debug.Log("BURN");
        }
        SoundManager.Play3DSound(SoundManager.Sound.SpecialAttack, gameObject);
    }
}
