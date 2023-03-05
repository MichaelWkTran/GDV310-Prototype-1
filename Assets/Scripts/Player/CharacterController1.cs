using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController1 : MonoBehaviour
{
    bool info;
    public static bool play;    

    public CharacterController m_Controller;
    public MouseLook m_Look;

    private AudioSource dashAudio;

    public float moveSpeed = 4.0f;
    public Vector3 velocity;
    private Vector3 dashVel;
    public float dashSpeed;
    public float dashTimer;
    public bool grounded;
    public bool sprinting;

    public float m_Gravity = 40.0f;
    public float m_JumpSpeed = 12.0f;
    public float m_SprintModifier = 2.0f;

    private bool canDash;

    // player attacking
    float timer = 1.0f;

    public GameObject cube;

    PlayerSystem playerSystem;


    // Start is called before the first frame update
    void Start()
    {
        info = true;
        play = false;

        canDash = true;

        dashAudio = GetComponent<AudioSource>();
        playerSystem = FindObjectOfType<PlayerSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        
        play = true;
          

        if (play)
        {
            // player movement

            float x = 0;
            if (Input.GetKey(KeyCode.A))
            {
                x -= 1.0f;
            }
            if (Input.GetKey(KeyCode.D))
            {
                x += 1.0f;
            }

            float z = 0;
            if (Input.GetKey(KeyCode.S))
            {
                z -= 1.0f;
            }
            if (Input.GetKey(KeyCode.W))
            {
                z += 1.0f;
            }

            // jumping
            if (grounded && Input.GetKeyDown(KeyCode.Space))
            {
                velocity.y = m_JumpSpeed;
            }

            // sprinting
            if (Input.GetKey(KeyCode.LeftShift))
            {
                sprinting = true;
            }
            else
            {
                sprinting = false;
            }

            // dash
            dashVel = Vector3.Lerp(dashVel, new Vector3(0.0f, 0.0f, 0.0f), dashTimer * Time.deltaTime);

            if (canDash && Input.GetKeyDown(KeyCode.LeftControl))
            {
                dashVel = m_Look.transform.forward;
                dashVel.y = 0.0f;

                dashAudio.PlayOneShot(GetComponent<AudioSource>().clip);

                StartCoroutine(DashCoolDown());
            }

            Vector3 inputMove = new Vector3(x, 0.0f, z);
            inputMove = Quaternion.Euler(0.0f, m_Look.m_Spin, 0.0f) * inputMove;

            float sprintMod = 1.0f;
            if (sprinting && !(Input.GetKey(KeyCode.S))) // calculating sprint. can't sprint while moving backwards
            {
                sprintMod = 2.0f;
            }

            velocity.x = inputMove.x * moveSpeed * sprintMod;
            velocity.y -= m_Gravity * Time.deltaTime;
            velocity.z = inputMove.z * moveSpeed * sprintMod;

            // updated velocity to include dashing
            velocity = velocity + dashVel * dashSpeed;

            // applies movement to the controller
            m_Controller.Move(velocity * Time.deltaTime);


            // player attack
            timer += Time.deltaTime;
        }

        // player collisions with ground
        if ((m_Controller.collisionFlags & CollisionFlags.Below) != 0)
        {
            grounded = true;
            velocity.y = -1.0f;
        }
        else
        {
            grounded = false;
        }

        if ((m_Controller.collisionFlags & CollisionFlags.Above) != 0)
        {
            velocity.y = -1.0f;
        }
    }

    void OnCollisionEnter(Collision _collision)
    {
        //Damage the Player
        if (HitCollider.OnEnter(_collision.gameObject.GetComponent<HitCollider>(), true))
            playerSystem.DamagePlayer(_collision.gameObject.GetComponent<HitCollider>().damage);
    }

    void OnCollisionStay(Collision _collision)
    {
        //Damage the Player
        if (HitCollider.OnEnter(_collision.gameObject.GetComponent<HitCollider>(), true))
            playerSystem.DamagePlayer(_collision.gameObject.GetComponent<HitCollider>().damage * Time.deltaTime);
    }

    void OnTriggerEnter(Collider _other)
    {
        //Damage the Player
        if (HitCollider.OnEnter(_other.GetComponent<HitCollider>(), true))
            playerSystem.DamagePlayer(_other.GetComponent<HitCollider>().damage);
    }

    void OnTriggerStay(Collider _other)
    {
        //Damage the Player
        if (HitCollider.OnEnter(_other.GetComponent<HitCollider>(), true))
            playerSystem.DamagePlayer(_other.GetComponent<HitCollider>().damage * Time.deltaTime);
    }

    IEnumerator DashCoolDown()
    {
        canDash = false;
        yield return new WaitForSeconds(3);
        canDash = true;
    }
}
