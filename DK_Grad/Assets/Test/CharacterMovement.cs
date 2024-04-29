using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private float runSpeed = 10f;
    [Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;
    [SerializeField] private float m_JumpForce = 5f;

    private Rigidbody m_Rigidbody;
    private float horizontalMove = 0f;
    private bool jump = false;
    
    private Vector3 m_Velocity = Vector3.zero;


    private void Awake()
    {
        m_Rigidbody = this.GetComponent<Rigidbody>();
    }

    void Start()
    {

    }


    void Update()
    {
        horizontalMove = Input.GetAxis("Horizontal");
        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
        }
    }

    private void FixedUpdate()
    {
        
        float abs_val = Math.Abs(horizontalMove);
        this.GetComponent<Animator>().SetFloat("Movement", abs_val);
        if (horizontalMove > 0.3f)
        {
            transform.rotation =
                Quaternion.Euler(transform.rotation.eulerAngles.x, 0f, transform.rotation.eulerAngles.z);
        }
        else if (horizontalMove < -0.3f)
        {
            transform.rotation =
                Quaternion.Euler(transform.rotation.eulerAngles.x, 180f, transform.rotation.eulerAngles.z);
        }
        
        Move(horizontalMove, false, jump);
        jump = false;
    }


    private void Move(float move, bool crouch, bool jump)
    {
        /*if (!crouch)
        {
            
        }
        else
        {
            
        }*/

        Vector3 targerVelocity = new Vector3(m_Rigidbody.velocity.x, m_Rigidbody.velocity.y, move * 10f);
        m_Rigidbody.velocity = Vector3.SmoothDamp(m_Rigidbody.velocity, targerVelocity, ref m_Velocity, m_MovementSmoothing);

        if (jump)
        {
            m_Rigidbody.AddForce(new Vector3(0f, m_JumpForce, 0f));
        }
    }
}
