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
    [SerializeField] Transform target;
    [SerializeField] Animator animator;

    void Start()
    {
        StartCoroutine(RecalculateAttackRadius());
    }

    void Update()
    {
        Grounded = !agent.isOnOffMeshLink;
        Vector3 targetVector = target.position - transform.position;
        float outerRadius = innerAttackRadius + outerAttackDistance;
        animator.SetLayerWeight(animator.GetLayerIndex("Attack Layer"), 0.0f);

        //Back from the target if too close
        if (targetVector.sqrMagnitude < innerAttackRadius*innerAttackRadius)
            agent.destination = transform.position - (targetVector.normalized * 5.0f);
        //Approach the target if too far away
        else if (targetVector.sqrMagnitude > outerRadius*outerRadius)
            agent.destination = target.position;
        //Attack the target if in range
        else
        {
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

    void OnAnimatorMove()
    {
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
        GameObject projectile = Instantiate(projectilePrefab.gameObject, firePoint.position, Quaternion.identity);
        projectile.GetComponent<Rigidbody>().velocity = (target.position - transform.position).normalized * fireSpeed;
        Destroy(projectile, projectileLifetime);
    }
}
