using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierMovement : MonoBehaviour
{
    public float speed = 3f;
    Vector3 movement;
    Animator anim;
    Rigidbody playerRigidbody;
    int floorMask;
    float camRayLength = 1000f;
    CharacterController controller;
    Camera cam;
    [SerializeField]
    float rotationSmoothTime;
    [Header("Gravity")]
    [SerializeField]
    float gravity = 9.8f;
    [SerializeField]
    float gravityMultiplier = 2;
    [SerializeField]
    float groundedGravity = -0.5f;
    [SerializeField]
    float jumpHeight = 3f;
    float currentAngle;
    float currentAngleVelocity;
    float velocityY;
    // Use this for initialization
    void Awake()
    {
        controller = GetComponent<CharacterController>();
        cam = Camera.main;

        floorMask = LayerMask.GetMask("Floor");
        //Debug.Log("LayerMask " + LayerMask.GetMask ("Floor"));
        anim = GetComponent<Animator>();
        playerRigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        HandleMovement();
        HandleGravityAndJump();
         Turning();
    }


    private void HandleMovement()
    {
        //capturing Input from Player
        Vector3 movement = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;
        if (movement.magnitude >= 0.1f)
        {
     
                float targetAngle = targetAngleY;
                //float targetAngle = Mathf.Atan2(movement.x, movement.z) * Mathf.Rad2Deg + cam.transform.eulerAngles.y;
                currentAngle = Mathf.SmoothDampAngle(currentAngle, targetAngle, ref currentAngleVelocity, rotationSmoothTime);
                transform.rotation = Quaternion.Euler(0, currentAngle, 0);
                Vector3 rotatedMovement = Quaternion.Euler(0, targetAngle, 0) * Vector3.forward;
                controller.Move(rotatedMovement * speed * Time.deltaTime);
            

        }
    }

    void HandleGravityAndJump()
    {
        if (controller.isGrounded && velocityY < 0f)
            velocityY = groundedGravity;
        if (controller.isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            velocityY = Mathf.Sqrt(jumpHeight * 2f * gravity);
        }
        velocityY -= gravity * gravityMultiplier * Time.deltaTime;
        controller.Move(Vector3.up * velocityY * Time.deltaTime);
    }

    // void FixedUpdate()
    // {
    //     float h = Input.GetAxisRaw("Horizontal");
    //     float v = Input.GetAxisRaw("Vertical");

    //     Move(h, v);
    //     Turning();
    //     Animating(h, v);
    // }

    float targetAngleY;
    void Turning()
    {
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        //Debug.Log("camRay " + camRay);
        RaycastHit floorHit;
        //Debug.Log("LayerMask " + LayerMask.LayerToName(256));
        //Debug.Log("LayerMask " + LayerMask.NameToLayer("Floor"));
        //print (Physics.Raycast (camRay, out floorHit, camRayLength, floorMask));
        //if (Physics.Raycast (camRay, out floorHit, camRayLength, floorMask)) {
        if (Physics.Raycast(camRay, out floorHit, camRayLength, floorMask))
        {
            //Debug.Log("floorHit " + floorHit);
            Vector3 playerToMouse = floorHit.point - transform.position;
            playerToMouse.y = 0f;

            Quaternion newRotation = Quaternion.LookRotation(playerToMouse);
            targetAngleY = newRotation.eulerAngles.y;
            playerRigidbody.MoveRotation(newRotation);
            //print ("byebye");
        }
    }

    void Animating(float h, float v)
    {
        bool running = h != 0f || v != 0f;
        anim.SetBool("IsRunning", running);
    }
}
