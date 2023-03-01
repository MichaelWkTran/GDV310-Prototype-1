using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wand : MonoBehaviour
{
    public GameObject projectile;
    public GameObject specialProjectile;
    public GameObject player;
    public GameObject wandEnd;

    public float forceStrength = 10.0f;


    // delay wand shoot
    float timer = 1.0f;




    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        // player attack
        timer += Time.deltaTime;

        if (Input.GetMouseButtonDown(0) && timer > 1)
        {
            BasicWandAttack();
            timer = 0;
        }

        if (Input.GetMouseButtonDown(1) && timer > 3)
        {
            SpecialWandAttack();
            timer = 0;
        }


    }


    void BasicWandAttack()
    {
        GameObject copy = (GameObject)Instantiate(projectile, wandEnd.transform.position + (wandEnd.transform.forward * 1.001f), Quaternion.identity); // copy of basic attack object
        Rigidbody rb = copy.AddComponent(typeof(Rigidbody)) as Rigidbody;                                                                              // add rigid body to new object
        rb.freezeRotation = true;                                                                                                                      // no rotation
        rb.useGravity = false;                                                                                                                         // no gravity
        rb.AddForce(player.transform.forward * forceStrength, ForceMode.Impulse);                                                                      // add orce to rb 
        Destroy(copy, 3);                                                                                                                              // destroy object 
    }


    void SpecialWandAttack()
    {
        GameObject copy = (GameObject)Instantiate(specialProjectile, new Vector3(player.transform.position.x, player.transform.position.y - 1.0f, player.transform.position.z), Quaternion.identity);
        Destroy(copy, 5);
    }
}
