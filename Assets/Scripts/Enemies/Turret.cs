using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : Enemy
{
    [SerializeField] float fireRate;
    float fireTime;
    [SerializeField] float fireSpeed;
    [SerializeField] float projectileLifetime;
    [SerializeField] Rigidbody projectilePrefab;
    [SerializeField] float headSlerpFactor;

    [SerializeField] bool active = false;
    public bool Active
    {
        get { return active; }
        set
        {
            active = value;
            //Play activate or disable animation
        }
    }
    [SerializeField] bool lookAtPlayer = true;

    [SerializeField] Transform head; //The object that tilts to look at the player
    [SerializeField] Transform firePoint; //The point from which the projectile is fired from
    CharacterController1 player; //The target that the turret looks and shoots at

    void Update()
    {
        //Dont perform any actions if the turret is not active
        if (!Active) return;

        //Dont perform any actions if the target is not defined
        if (!FindPlayer(ref player)) return;

        //Tilt the head to look at the player
        if (lookAtPlayer) head.rotation = Quaternion.Slerp(head.rotation, Quaternion.LookRotation(player.transform.position - head.position), headSlerpFactor * Time.deltaTime);

        //Shoot the projectile
        fireTime += Time.deltaTime;
        if (fireTime > fireRate)
        {
            fireTime = 0;
            GameObject projectile = Instantiate(projectilePrefab.gameObject, firePoint.position, Quaternion.identity);
            projectile.GetComponent<Rigidbody>().velocity = head.forward * fireSpeed;
            Destroy(projectile, projectileLifetime);
        }
    }
}
