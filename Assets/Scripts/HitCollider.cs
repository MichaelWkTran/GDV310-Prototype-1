using UnityEngine;

//This class is used for enemies and players and can be used for projectiles
[RequireComponent(typeof(Collider))]
public class HitCollider : MonoBehaviour
{
    public enum ContactBehaviour
    {
        HitOnEnter, //Hit the target when first making contact
        HitOnStay, //Hit the target as long as they are in the collider
        DestroyOnHit //Same as HitOnEnter but the gameObject of the collider is destroyed
    }

    public float damage; //How much damage is applied to the target when hit
    public bool isPlayerCollider; //Whether the collider belongs to the player
    [SerializeField] protected ContactBehaviour contactBehaviour; //How would the hit collider behave
    public bool destroyOnCollision; //Destroy Object when colliding with another object

    void OnCollisionEnter(Collision _collision)
    {
        if (_collision.gameObject.isStatic && destroyOnCollision) Destroy(gameObject);
    }

    void OnTriggerEnter(Collider _other)
    {
        if (_other.gameObject.isStatic && destroyOnCollision) Destroy(gameObject);
    }
    
    //OnEnter and OnStay returns true when damage should be applied in those states
    bool OnEnter()
    {
        //Destroy on Hit
        if (contactBehaviour == ContactBehaviour.DestroyOnHit) Destroy(gameObject);

        //Apply Damage on Hit
        if (contactBehaviour == ContactBehaviour.HitOnEnter || contactBehaviour == ContactBehaviour.DestroyOnHit) return true;

        //No Damage is Applied
        return false;
    }

    bool OnStay()
    {
        //Apply Damage on Stay
        return contactBehaviour == ContactBehaviour.HitOnStay;
    }

    //Called in OnCollisionEnter and OnTriggerEnter
    static public bool OnEnter(HitCollider _hitCollider, bool _isPlayer)
    {
        //Get Hit Collider
        if (_hitCollider == null) return false;

        //Check whether the collider is not from the same side
        if (_hitCollider.isPlayerCollider == _isPlayer) return false;

        //Damage the Enemy
        return _hitCollider.OnEnter();
    }

    static public bool OnStay(HitCollider _hitCollider, bool _isPlayer)
    {
        //Get Hit Collider
        if (_hitCollider == null) return false;

        //Check whether the collider is not from the same side
        if (_hitCollider.isPlayerCollider == _isPlayer) return false;

        //Damage the Enemy
        return _hitCollider.OnStay();
    }
}
