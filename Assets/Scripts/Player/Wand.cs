using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Wand : MonoBehaviour
{
    public GameObject projectile;
    public GameObject specialProjectile;
    public GameObject player;
    public GameObject wandEnd;
    public Animator animator;

    public float charge;
    public Slider sliderBar;

    //public Image specialChargeBar;

    public float forceStrength = 10.0f; // strength of force

    public static float wandCharge; // how much charge for player to use special attack
    private float wandChargeMax = 3; // maximum number wandCharge can be

    // delay wand shoot
    float timer = 1.0f;




    // Start is called before the first frame update
    void Start()
    {
        wandCharge = 3; // special is charged at start of game
    }

    // Update is called once per frame
    void Update()
    {
        // player attack
        timer += Time.deltaTime;

        if (Input.GetMouseButtonDown(0) && timer > 1 && PlayerSystem.pHealth >= 0)
        {
            BasicWandAttack();
            timer = 0;
        }

        if (Input.GetMouseButtonDown(1) && Wand.wandCharge == 3 && PlayerSystem.pHealth >= 0)
        {
            SpecialWandAttack();
            timer = 0;
        }
        sliderBar.value = Wand.wandCharge * 33.333f;
        //specialChargeBar.fillAmount = Wand.wandCharge / wandChargeMax;
    }


    void BasicWandAttack()
    {
        if (!PauseMenu.isPaused)
        {
            animator.SetTrigger("Attack");
            GameObject copy = (GameObject)Instantiate(projectile, wandEnd.transform.position + (wandEnd.transform.forward * 1.01f), Quaternion.identity); // copy of basic attack object
            Rigidbody rb = copy.AddComponent(typeof(Rigidbody)) as Rigidbody;                                                                              // add rigid body to new object
            rb.freezeRotation = true;                                                                                                                      // no rotation
            rb.useGravity = false;                                                                                                                         // no gravity
            rb.AddForce(player.transform.forward * forceStrength, ForceMode.Impulse);                                                                      // add orce to rb 
            Destroy(copy, 3);                                                                                                                              // destroy object 
        }                                                                                                                       
    }


    void SpecialWandAttack()
    {
        if (!PauseMenu.isPaused)
        {
            animator.SetTrigger("Attack");
            StartCoroutine(ScreenShake());
            GameObject copy = (GameObject)Instantiate(specialProjectile, new Vector3(player.transform.position.x, player.transform.position.y - 1.0f, player.transform.position.z), Quaternion.identity);
            Destroy(copy, 5);
            Wand.wandCharge = 0;
        }
    }


    IEnumerator ScreenShake()
    {
        CameraShake.isShaking = true;
        yield return new WaitForSeconds(2);
        CameraShake.isShaking = false;
    }
}
