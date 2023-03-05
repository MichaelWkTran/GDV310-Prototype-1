using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
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

    Vector3 hitPosition;
    Vector3 hitForce;

    void Start()
    {
        //Disable ragdoll
        foreach(Rigidbody rigidBody in GetComponentsInChildren<Rigidbody>())
        {
            rigidBody.isKinematic = true;
            rigidBody.GetComponent<Collider>().enabled = false;
        }
    }

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

    protected override void KillEnemy() 
    {
        //Enable ragdoll
        foreach (Rigidbody rigidBody in GetComponentsInChildren<Rigidbody>())
        {
            rigidBody.isKinematic = false;
            rigidBody.GetComponent<Collider>().enabled = true;

            //Apply Force to ragdoll
            rigidBody.AddForceAtPosition(hitForce*50.0f, hitPosition, ForceMode.Impulse);
        }

        //Disable components that would interfere with the ragdoll
        agent.enabled = false;
        animator.enabled = false;
        GetComponent<Collider>().enabled = false;
        hitBox.isTrigger = false;
        enabled = false;

        //Destroy the Enemy
        Destroy(gameObject, 10.0f);
    }
}
