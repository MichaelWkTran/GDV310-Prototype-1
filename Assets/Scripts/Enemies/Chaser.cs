using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Chaser : Enemy
{
    [SerializeField] float attackRadius; //The radius from which the enemy would attack the target
    [SerializeField] bool showAttackRadius;
    [SerializeField] Collider hitBox;
    [SerializeField] bool grounded;
    bool Grounded
    {
        get { return grounded; }
        set
        {
            //If grounded has changed, then trigger land or jump animation
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

    void Update()
    {
        //Set whether the enemy is grounded
        Grounded = !agent.isOnOffMeshLink;

        //Dont perform any actions if the target is not defined
        if (!FindPlayer(ref player)) return;

        //Move the enemy towards the target
        agent.destination = player.transform.position;

        //Only enable hitbox when attacking
        hitBox.enabled = animator.GetCurrentAnimatorStateInfo(0).IsName("Attack");

        //Trigger attack animation if close enough to the target
        if (Grounded && (player.transform.position - transform.position).sqrMagnitude < attackRadius * attackRadius)
        {
            //Set attack animation if the player is not in the attack animation
            if (!hitBox.enabled) animator.SetTrigger("Attack");
        }
    }

    void LateUpdate()
    {
        //Set whether the player is in idle or in moving animation
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

    void OnDrawGizmosSelected()
    {
        if (showAttackRadius)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, attackRadius);
        }
    }
}
