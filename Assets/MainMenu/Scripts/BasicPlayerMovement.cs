using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicPlayerMovement : MonoBehaviour
{
    public bool m_cursorLocked = true;

    public float m_Spin = 0.0f;
    public float m_Tilt = 0.0f;
    public float m_Lean = 0.0f;

    public Vector2 m_TiltExtents = new Vector2(-85.0f, 85.0f);

    public float m_Sensitivity = 2.0f;


    public CharacterController m_Controller;

    public float m_MoveSpeed = 4.0f;
    public float m_SprintModifier = 2.0f;
    public bool m_Sprinting;

    public Vector3 m_Velocity;
    public bool m_Grounded;

    public float m_Gravity = 40.0f;
    public float m_JumpSpeed = 12.0f;

    void Update()
    {
        MouseUpdate();
        float x = 0.0f;
        if ((Input.GetKey(KeyCode.A)) || (Input.GetKey(KeyCode.LeftArrow)))
        {
            x -= 1.0f;
        }
        if ((Input.GetKey(KeyCode.D)) || (Input.GetKey(KeyCode.RightArrow)))
        {
            x += 1.0f;
        }
        float z = 0.0f;
        if ((Input.GetKey(KeyCode.S)) || (Input.GetKey(KeyCode.DownArrow)))
        {
            z -= 1.0f;
        }
        if ((Input.GetKey(KeyCode.W)) || (Input.GetKey(KeyCode.UpArrow)))
        {
            z += 1.0f;
        }

        if (m_Grounded && Input.GetKeyDown(KeyCode.Space))
        {
            m_Velocity.y = m_JumpSpeed;
        }
        if (Input.GetKey(KeyCode.LeftShift))
        {
            m_Sprinting = true;
        }
        else
        {
            m_Sprinting = false;
        }

        Vector3 inputMove = new Vector3(x, 0.0f, z);
        inputMove = Quaternion.Euler(0.0f, m_Spin, 0.0f) * inputMove;

        float sprintMod = 1.0f;
        if (m_Sprinting)
        {
            sprintMod = m_SprintModifier;
        }
        m_Velocity.x = inputMove.x * m_MoveSpeed * sprintMod;
        m_Velocity.y -= m_Gravity * Time.deltaTime;
        m_Velocity.z = inputMove.z * m_MoveSpeed * sprintMod;

        m_Controller.Move(m_Velocity * Time.deltaTime);
        if ((m_Controller.collisionFlags & CollisionFlags.Below) != 0)
        {
            m_Grounded = true;
            m_Velocity.y = -1.0f;
        }
        else
        {
            m_Grounded = false;
        }
        if ((m_Controller.collisionFlags & CollisionFlags.Above) != 0)
        {
            m_Velocity.y = -2.0f;

        }
    }
    void LockCursor()
    {
        Cursor.visible = !m_cursorLocked;
        Cursor.lockState = m_cursorLocked ? CursorLockMode.Locked : CursorLockMode.None;

    }
    void Start()
    {
        LockCursor();
    }

    void MouseUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            m_cursorLocked = !m_cursorLocked;
            LockCursor();
        }
        float x = Input.GetAxisRaw("Mouse X");
        float y = Input.GetAxisRaw("Mouse Y");
        m_Spin += x * m_Sensitivity;
        m_Tilt -= y * m_Sensitivity;

        m_Tilt = Mathf.Clamp(m_Tilt, m_TiltExtents.x, m_TiltExtents.y);

        transform.localEulerAngles = new Vector3(m_Tilt, m_Spin, m_Lean);
    }
}
