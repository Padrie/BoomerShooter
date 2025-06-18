using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Camera playerCamera;
    
    public CharacterController characterController;

    public Transform playerBody;
    
    public float gravity = -9.81f;
    
    public Vector3 velocity;
    
    public bool isGrounded;
    
    public LayerMask groundLayer;
    public float groundDistance = 0.4f;

    public float WalkSpeed = 10f;
    public float RunSpeed = 14f;
    public float JumpHeight = 5f;

    private float rotX;
    private float rotY;
    public float sensitivity = 400;
    
    private float inputX;
    private float inputZ;
    
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        CheckGrounded();
        
        HandleInput();
        
    }

    void FixedUpdate()
    {
        HandleGravity();
        
        HandleWalking();
    }

    public void HandleInput()
    {
        inputX = Input.GetAxis("Horizontal");
        inputZ = Input.GetAxis("Vertical");
    }

    public void HandleGravity()
    {
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        
        velocity.y += gravity * Time.deltaTime;
        
        characterController.Move(velocity * Time.deltaTime);
    }

    public void HandleWalking()
    {
        Vector3 move = transform.right * inputX + transform.forward * inputZ;
        
        characterController.Move(move * WalkSpeed * Time.deltaTime);
        
    }

    public void HandleJump()
    {
        velocity.y = Mathf.Sqrt(JumpHeight * -2f * gravity);
    }

    public void HandleCrouching()
    {
        
    }

    public void HandleDash()
    {
        
    }

    public void CheckGrounded()
    {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, groundDistance, groundLayer);
        
    }

    public void HandleCameraMovement()
    {
        //Mouse Input
        rotX += Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        rotY -= Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;
        
        //Limit rot
        rotY = Mathf.Clamp(rotY, -90, 90);
        
        //Rotate Camera and Player(Body)
        playerCamera.transform.localRotation = Quaternion.Euler(rotY, 0 , 0);
        
        playerBody.Rotate(Vector3.up * rotX);
        transform.rotation = Quaternion.Euler(0, rotX, 0);
    }
    

}
