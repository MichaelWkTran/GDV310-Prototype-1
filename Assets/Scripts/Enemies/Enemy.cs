using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] protected float health;

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
