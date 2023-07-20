using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEntity : BaseEntity
{
    [SerializeField]
    LayerMask m_FloorMask;
    float m_CamLength = 1000f;

    protected override void Update()
    {
        HandleMovement();
        UpdateAniState();
    }

    private void HandleMovement()
    {
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        //Debug.Log("camRay " + camRay);
        RaycastHit floorHit;
        float targetAnlgY = 0f;
        if (Physics.Raycast(camRay, out floorHit, m_CamLength, m_FloorMask))
        {
            //Debug.Log("floorHit " + floorHit);
            Vector3 playerToMouse = floorHit.point - transform.position;
            playerToMouse.y = 0f;

            Quaternion newRotation = Quaternion.LookRotation(playerToMouse);
            targetAnlgY = newRotation.eulerAngles.y;
            transform.localRotation = Quaternion.Euler(0f, targetAnlgY, 0f);
            //m_RigidBody.MoveRotation(newRotation);
        }

        m_HInput = Input.GetAxisRaw("Horizontal");
        m_VInput = Input.GetAxisRaw("Vertical");
        //capturing Input from Player
        Vector3 movement = new Vector3(m_HInput, 0, m_VInput).normalized;
        if (movement.magnitude >= 0.1f)
        {
            //float targetAngle = Mathf.Atan2(movement.x, movement.z) * Mathf.Rad2Deg + cam.transform.eulerAngles.y;
            //m_CurAngle = Mathf.SmoothDampAngle(m_CurAngle, targetAngle, ref m_CurAngleVelocity, m_RotSmoothTime);
            //transform.rotation = Quaternion.Euler(0, m_CurAngle, 0);
            Vector3 rotatedMovement = Quaternion.Euler(0, targetAnlgY, 0) * Vector3.forward;
            m_ChaController.Move(rotatedMovement * m_Speed * Time.deltaTime);
        }
    }
    float m_HInput;
    float m_VInput;
    protected override void UpdateAniState()
    {
        m_ChaAnimator.SetBool("IsRunning", Mathf.Abs(m_HInput) > 0f || Mathf.Abs(m_VInput) > 0);
    }
}
