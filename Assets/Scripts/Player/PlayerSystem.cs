using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSystem : MonoBehaviour
{
    //Health Based floats
    public float pHealth;
    public float pMaxHealth = 100.0f;
    public float pMinHealth = 0.0f;

    //Weapon Damage Based floats
    public float mwDamage;
    public float rwDamage;

    //Weapon Active Check
    public bool wandActive;
    public Image wand;
    public Image sword;

    //Buff timer floats
    public float bTimer;
    public float bTimerMax;

    // Start is called before the first frame update
    void Start()
    {
        //Sets health to max
        pHealth = pMaxHealth;

        //Sets primary weapon to true
        wandActive = true;
    }

    // Update is called once per frame
    void Update()
    {
        //Changes weapon active
        if (Input.GetMouseButtonDown(0))
        {
            wandActive = !wandActive;
        }

        //Displays active weapon
        if (wandActive)
        {
            wand.enabled = true;
            sword.enabled = false;
        }
        else if (!wandActive)
        {
            wand.enabled = false;
            sword.enabled = true;
        }
    }
}
