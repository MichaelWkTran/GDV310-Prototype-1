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
    public Slider sliderHealth;

    //Weapon Damage Based floats
    public float mwDamage;
    public float mwDamageNormal = 5.0f;
    public float mwDamageBoosted = 20.0f;
    public float rwDamage;
    public float rwDamageNormal = 5.0f;
    public float rwDamageBoosted = 20.0f;

    //Weapon Active Check
    public bool wandActive;
    public Image wand;
    public Image sword;

    //Buff timer floats
    public float bTimerRegen;
    public float bTimerRegenMax = 15.0f;
    public float bTimerDMGBoost;
    public float bTimerDMGBoostMax = 15.0f;     //Change some of these to private


    public Slider sliderDValue;
    public Slider sliderRValue;
    public Slider sliderJValue;

    //Weapon Attack values
    public float specialCharge;
    public float specialChargeMax = 100.0f;


    // Start is called before the first frame update
    void Start()
    {
        specialCharge = 0.0f;

        //Sets health to max
        pHealth = pMaxHealth;

        mwDamage = mwDamageNormal;
        rwDamage = rwDamageNormal;

        //Sets primary weapon to true
        wandActive = true;
    }

    public void Health()
    {
        sliderHealth.value = pHealth;
    }

    public void DMGSlider()
    {
        sliderDValue.value = bTimerDMGBoost;
    }

    public void RegenSlider ()
    {
        sliderRValue.value = bTimerRegen;
    }

    public void JumpSlider()
    {
        sliderJValue.value = bTimerRegen;
    }

    // Update is called once per frame
    void Update()
    {
        Health();
        RegenSlider();
        DMGSlider();
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
        
        if(bTimerDMGBoost > 0.0f)
        {
            mwDamage = mwDamageBoosted;
            rwDamage = rwDamageBoosted;

            bTimerDMGBoost -= 1 * Time.deltaTime;
        }
        else if(bTimerDMGBoost <= 0.0f)
        {
            mwDamage = mwDamageNormal;
            rwDamage = rwDamageNormal;

            bTimerDMGBoost = 0.0f;
        }

        if(bTimerRegen > 0.0f)
        {
            if(pHealth < pMaxHealth && pHealth > pMinHealth)
            {
                pHealth += 5 * Time.deltaTime;
            }
            bTimerRegen -= 1 * Time.deltaTime;
        }
        else if(bTimerRegen <= 0.0f)
        {
            bTimerRegen = 0.0f;
        }
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "RegenBoost")
        {
            bTimerRegen = bTimerRegenMax;
            Destroy(other);
        }
        if (other.tag == "DMGBoost")
        {
            bTimerDMGBoost = bTimerDMGBoostMax;
            Destroy(other);
        }
    }

    public void DamagePlayer(float value)
    {
        pHealth -= value;
    }
}
