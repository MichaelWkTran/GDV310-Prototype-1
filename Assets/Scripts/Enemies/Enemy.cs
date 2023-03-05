using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] protected float health;

    protected void OnCollisionEnter(Collision _collision)
    {
        //Damage the Enemy
        if (HitCollider.OnEnter(_collision.gameObject.GetComponent<HitCollider>(), false))
            DamageEnemy(_collision.gameObject.GetComponent<HitCollider>().damage);
    }

    protected void OnCollisionStay(Collision _collision)
    {
        //Damage the Enemy
        if (HitCollider.OnEnter(_collision.gameObject.GetComponent<HitCollider>(), false))
            DamageEnemy(_collision.gameObject.GetComponent<HitCollider>().damage * Time.deltaTime);
    }

    protected void OnTriggerEnter(Collider _other)
    {
        //Damage the Enemy
        if (HitCollider.OnEnter(_other.GetComponent<HitCollider>(), false))
            DamageEnemy(_other.GetComponent<HitCollider>().damage);
    }

    protected void OnTriggerStay(Collider _other)
    {
        //Damage the Enemy
        if (HitCollider.OnEnter(_other.GetComponent<HitCollider>(), false))
            DamageEnemy(_other.GetComponent<HitCollider>().damage * Time.deltaTime);
    }

    public virtual void DamageEnemy(float _value)
    {
        health -= _value;
        if (health <= 0) KillEnemy();
    }

    protected virtual void KillEnemy()
    {
        Destroy(gameObject);
    }

    protected bool FindPlayer(ref CharacterController1 _player)
    {
        //Check whether the player is already found, if so then return true
        if (_player != null) return true;

        //Find the player
        _player = FindObjectOfType<CharacterController1>();

        //If the player is not found return false
        if (_player == null) return false;
        //Otherwise return true
        else return true;
    }
}
