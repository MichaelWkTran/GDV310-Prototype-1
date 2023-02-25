using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] protected float health;

    protected virtual void DamageEnemy(float _value)
    {
        health -= _value;
        if (health <= 0) KillEnemy();
    }

    protected virtual void KillEnemy()
    {
        Destroy(gameObject);
    }
}
