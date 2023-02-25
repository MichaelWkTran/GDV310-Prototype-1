using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Chaser : Enemy
{
    [SerializeField] float attackRadius;
    [SerializeField] bool showAttackRadius;
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

    void Update()
    {
        Grounded = !agent.isOnOffMeshLink;
        agent.destination = target.position;

        //Trigger attack animation
        if (Grounded && (target.position - transform.position).sqrMagnitude < attackRadius * attackRadius)
        {
            if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
            {
                animator.SetTrigger("Attack");
            }
        }
    }

    void LateUpdate()
    {
        animator.SetBool("Moving", agent.velocity.sqrMagnitude > 0);
        
    }

    void OnAnimatorMove()
    {
        //Apply root motion if the player is not attacking or not in landing animation
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Attack") || animator.GetCurrentAnimatorStateInfo(0).IsName("Land"))
        {
            agent.velocity = animator.velocity;
            transform.rotation *= Quaternion.Euler(animator.angularVelocity);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (showAttackRadius)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, attackRadius);
        }
    }
}
