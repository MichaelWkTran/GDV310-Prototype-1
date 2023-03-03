using System.Collections.Generic;
using UnityEngine;

//This class is used for enemies and players and can be used for projectiles
[RequireComponent(typeof(Collider))]
public class HitCollider : MonoBehaviour
{
    public enum ContactBehaviour
    {
        HitOnEnter, //Hit the target when first making contact
        HitOnStay, //Hit the target as long as they are in the collider
        DestroyOnHit, //Same as HitOnEnter but the gameobject of the collider is destroyed
    }

    public float damage; //How much damage is applied to the target when hit
    [SerializeField] protected bool isPlayerCollider; //Whether the collider belongs to the player
    [SerializeField] protected ContactBehaviour contactBehaviour; //How would the hit collider behave
    protected HashSet<GameObject> collidingTargets = new HashSet<GameObject>(); //List of targets that the hit collider is in contact with

    void Update()
    {
        if (contactBehaviour == ContactBehaviour.HitOnStay)
        {
            //Apply continous damage to targets
            foreach (GameObject target in collidingTargets)
                DamageTarget(target, damage * Time.deltaTime);
        }
    }

    void OnCollisionEnter(Collision _collision)
    {
        OnEnter(_collision.gameObject);
    }

    void OnCollisionExit(Collision _collision)
    {
        OnExit(_collision.gameObject);
    }

    void OnTriggerEnter(Collider _other)
    {
        OnEnter(_other.gameObject);
    }

    void OnTriggerExit(Collider _other)
    {
        OnExit(_other.gameObject);
    }

    void OnEnter(GameObject _other)
    {
        //Set whether the hit collider is touching the target
        if (isPlayerCollider)
        {
            if (_other.transform.GetComponent<Enemy>()) collidingTargets.Add(_other);
            else return;
        }
        else
        {
            if (_other.transform.GetComponent<CharacterController1>()) collidingTargets.Add(_other);
            else return;
        }

        //Apply Damage on Hit
        if (contactBehaviour == ContactBehaviour.HitOnEnter || contactBehaviour == ContactBehaviour.DestroyOnHit)
            DamageTarget(_other, damage);

        //Destroy on Hit
        if (contactBehaviour == ContactBehaviour.DestroyOnHit) Destroy(gameObject);
    }

    void OnExit(GameObject _other)
    {
        //Set whether the hit collider has left the target
        if (isPlayerCollider) if (_other.transform.GetComponent<Enemy>()) collidingTargets.Remove(_other);
            else if (_other.transform.GetComponent<CharacterController1>()) collidingTargets.Remove(_other);
    }

    void DamageTarget(GameObject _target, float _damage)
    {
        //Damage the enemy
        {
            Enemy enemy = _target.GetComponent<Enemy>();
            if (enemy != null) enemy.DamageEnemy(_damage);
        }

        //Damage the player
        {
            CharacterController1 player = _target.GetComponent<CharacterController1>();
            PlayerSystem playerSystem = FindObjectOfType<PlayerSystem>();
            if (player != null && playerSystem != null) playerSystem.pHealth -= _damage;
        }
    }
}
