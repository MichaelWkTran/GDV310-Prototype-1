using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Projectile : MonoBehaviour
{
    public Image crossHair;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy") // enemy hit by attack
        {
            StartCoroutine(HitEnemy());
            StartCoroutine(ChangeColor());
        }
    }


    IEnumerator HitEnemy() // timer for when enemy is hit
    {
        Debug.Log("HITTTT");
        gameObject.transform.position = new Vector3(0, -1000, 0); // hide projectile so ChangeColor can run, then destroy projectile
        yield return new WaitForSeconds(0.6f);
        Destroy(gameObject); 
    }

    IEnumerator ChangeColor() // timer to change crosshair color when hitting enemy
    {
        crossHair.color = new Vector4(1, 0, 0, 1);
        yield return new WaitForSeconds(0.5f);
        crossHair.color = new Vector4(1, 1, 1, 1);
    }
}
