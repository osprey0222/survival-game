using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierMovement : MonoBehaviour
{
    public float m_Speed = 3f;
    Animator m_Animator;
    Rigidbody m_RigidBody;
    [SerializeField]
    LayerMask m_FloorMask;
    float m_CamLength = 1000f;
    CharacterController m_ChaController;
    [SerializeField]
    float m_RotSmoothTime;
    [Header("Gravity")]
    [SerializeField]
    float m_Gravity = 9.8f;
    [SerializeField]
    float m_GravityMultipler = 2;
    [SerializeField]
    float m_GroundedGravity = -0.5f;
    [SerializeField]
    float m_JumpHeight = 3f;
    float m_CurAngle;
    float m_CurAngleVelocity;
    float m_VelocityY;
    float m_TargetAngleY;
    float m_VerticalInput, m_HorizontalInput;

    // Use this for initialization
    void Awake()
    {
        m_ChaController = GetComponent<CharacterController>();

        //Debug.Log("LayerMask " + LayerMask.GetMask ("Floor"));
        m_Animator = GetComponent<Animator>();
        m_RigidBody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        Turning();
        HandleMovement();
        //HandleGravityAndJump();
        UpdateAnimationState();
    }

    private void HandleMovement()
    {
        m_HorizontalInput = Input.GetAxisRaw("Horizontal");
        m_VerticalInput = Input.GetAxisRaw("Vertical");
        //capturing Input from Player
        Vector3 movement = new Vector3(m_HorizontalInput, 0, m_VerticalInput).normalized;
        if (movement.magnitude >= 0.1f)
        {
            float targetAngle = m_TargetAngleY;
            //float targetAngle = Mathf.Atan2(movement.x, movement.z) * Mathf.Rad2Deg + cam.transform.eulerAngles.y;
            m_CurAngle = Mathf.SmoothDampAngle(m_CurAngle, targetAngle, ref m_CurAngleVelocity, m_RotSmoothTime);
            transform.rotation = Quaternion.Euler(0, m_CurAngle, 0);
            Vector3 rotatedMovement = Quaternion.Euler(0, targetAngle, 0) * Vector3.forward;
            m_ChaController.Move(rotatedMovement * m_Speed * Time.deltaTime);
        }
    }

    void HandleGravityAndJump()
    {
        if (m_ChaController.isGrounded && m_VelocityY < 0f)
            m_VelocityY = m_GroundedGravity;
        if (m_ChaController.isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            m_VelocityY = Mathf.Sqrt(m_JumpHeight * 2f * m_Gravity);
        }
        m_VelocityY -= m_Gravity * m_GravityMultipler * Time.deltaTime;
        m_ChaController.Move(Vector3.up * m_VelocityY * Time.deltaTime);
    }

    private void UpdateAnimationState()
    {
        bool bRunning = Math.Abs(m_HorizontalInput) > 0.1f || Math.Abs(m_VerticalInput) > 0.1f;
        m_Animator.SetBool("IsRunning", bRunning);
    }

    void Turning()
    {
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        //Debug.Log("camRay " + camRay);
        RaycastHit floorHit;
        if (Physics.Raycast(camRay, out floorHit, m_CamLength, m_FloorMask))
        {
            //Debug.Log("floorHit " + floorHit);
            Vector3 playerToMouse = floorHit.point - transform.position;
            playerToMouse.y = 0f;

            Quaternion newRotation = Quaternion.LookRotation(playerToMouse);
            m_TargetAngleY = newRotation.eulerAngles.y;
            m_RigidBody.MoveRotation(newRotation);
        }
    }
}
