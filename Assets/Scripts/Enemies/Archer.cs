using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Archer : Enemy
{
    [SerializeField] float innerAttackRadius; //The radius from which the enemy would attack the target
    [SerializeField] float outerAttackDistance; //The distance of the outer attack radius to the inner attack radius
    [SerializeField] float minAttackRadius; //The min range that the attack radius can be
    [SerializeField] float maxAttackRadius; //The max range that the attack radius can be
    [SerializeField] float attackMinRadiusRegenerationRate;
    [SerializeField] float attackMaxRadiusRegenerationRate;
    [SerializeField] bool showAttackRadius;
    [SerializeField] bool showAttackRadiusRange;

    [SerializeField] float lookAtSlerpFactor;

    [SerializeField] Transform firePoint; //The point from which the projectile is fired from
    [SerializeField] float fireRate;
    float fireTime;
    [SerializeField] float fireSpeed;
    [SerializeField] float projectileLifetime;
    [SerializeField] Rigidbody projectilePrefab;

    [SerializeField] bool grounded;
    bool Grounded
    {
        get { return grounded; }
        set
        {
            if (grounded != value)
            {
                if (value) animator.CrossFade("Land", 0.1f);
                else animator.CrossFade("Jump", 0.1f);
            }
            grounded = value;
        }
    }

    [SerializeField] NavMeshAgent agent;
    [SerializeField] Animator animator;
    CharacterController1 player;

    Vector3 hitPosition;
    Vector3 hitForce;

    void Start()
    {
        StartCoroutine(RecalculateAttackRadius());

        //Disable ragdoll
        foreach (Rigidbody rigidBody in GetComponentsInChildren<Rigidbody>())
        {
            rigidBody.isKinematic = true;
            rigidBody.GetComponent<Collider>().enabled = false;
        }
    }

    void Update()
    {
        //Dont perform any actions if the target is not defined
        if (!FindPlayer(ref player)) return;

        //Set whether the enemy is grounded
        Grounded = !agent.isOnOffMeshLink;

        //Set player to target variables
        Vector3 targetVector = player.transform.position - transform.position;
        float outerRadius = innerAttackRadius + outerAttackDistance;
        
        //Set the player attack idle to default
        animator.SetLayerWeight(animator.GetLayerIndex("Attack Layer"), 0.0f);

        //Back from the target if too close
        if (targetVector.sqrMagnitude < innerAttackRadius*innerAttackRadius)
            agent.destination = transform.position - (targetVector.normalized * 5.0f);
        //Approach the target if too far away
        else if (targetVector.sqrMagnitude > outerRadius*outerRadius)
            agent.destination = player.transform.position;
        //Attack the target if in range
        else
        {
            //Set the player attack idle to aiming
            animator.SetLayerWeight(animator.GetLayerIndex("Attack Layer"), 1.0f);

            //Shoot the projectile
            fireTime += Time.deltaTime;
            if (fireTime > fireRate)
            {
                fireTime = 0;
                animator.SetTrigger("Attack");
            }

            //Stop the enemy from moving
            agent.destination = transform.position;

            //Make the enemy look at the target
            Vector3 lookVector = targetVector; lookVector.y = 0.0f;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(lookVector), lookAtSlerpFactor * Time.deltaTime);
        }
    }

    void LateUpdate()
    {
        animator.SetBool("Moving", agent.velocity.sqrMagnitude > 0);
    }

    new protected void OnCollisionEnter(Collision _collision)
    {
        hitPosition = _collision.GetContact(0).point;
        hitForce = _collision.impulse;
        base.OnCollisionEnter(_collision);
    }

    new protected void OnCollisionStay(Collision _collision)
    {
        hitPosition = _collision.GetContact(0).point;
        hitForce = _collision.impulse;
        base.OnCollisionStay(_collision);
    }

    new protected void OnTriggerEnter(Collider _other)
    {
        hitPosition = _other.transform.position;
        hitForce = (transform.position - _other.transform.position).normalized;
        base.OnTriggerEnter(_other);
    }

    new protected void OnTriggerStay(Collider _other)
    {
        hitPosition = _other.transform.position;
        hitForce = (transform.position - _other.transform.position).normalized;
        base.OnTriggerStay(_other);
    }

    void OnAnimatorMove()
    {
        //Dont perform any actions if the target is not defined
        if (!FindPlayer(ref player)) return;

        //Apply root motion if the player is not attacking or not in landing animation
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Attack") || animator.GetCurrentAnimatorStateInfo(0).IsName("Recoil") || animator.GetCurrentAnimatorStateInfo(0).IsName("Land"))
        {
            agent.velocity = animator.velocity;
            transform.rotation *= Quaternion.Euler(animator.angularVelocity);
        }
    }

    void OnDrawGizmosSelected()
    {
        if (showAttackRadius)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, minAttackRadius);
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, maxAttackRadius);
        }

        if (showAttackRadiusRange)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, minAttackRadius);
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, minAttackRadius + outerAttackDistance);
        }
    }

    IEnumerator RecalculateAttackRadius()
    {
        innerAttackRadius = Random.Range(minAttackRadius, maxAttackRadius);

        yield return new WaitForSeconds(Random.Range(attackMinRadiusRegenerationRate, attackMaxRadiusRegenerationRate));

        StartCoroutine(RecalculateAttackRadius());
    }

    public void ShootProjectile()
    {
        //Dont perform any actions if the target is not defined
        if (!FindPlayer(ref player)) return;

        //Shoot the projectile
        GameObject projectile = Instantiate(projectilePrefab.gameObject, firePoint.position, Quaternion.identity);
        projectile.GetComponent<Rigidbody>().velocity = (player.transform.position - firePoint.position).normalized * fireSpeed;
        projectile.transform.rotation = Quaternion.LookRotation(projectile.GetComponent<Rigidbody>().velocity);
        Destroy(projectile, projectileLifetime);
    }

    protected override void KillEnemy()
    {
        //Enable ragdoll
        foreach (Rigidbody rigidBody in GetComponentsInChildren<Rigidbody>())
        {
            rigidBody.isKinematic = false;
            rigidBody.GetComponent<Collider>().enabled = true;

            //Apply Force to ragdoll
            rigidBody.AddForceAtPosition(hitForce * 50.0f, hitPosition, ForceMode.Impulse);
        }

        //Disable components that would interfere with the ragdoll
        agent.enabled = false;
        animator.enabled = false;
        GetComponent<Collider>().enabled = false;
        enabled = false;

        //Destroy the Enemy
        Destroy(gameObject, 10.0f);
    }
}
