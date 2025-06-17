using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    
    [Header("Objects")]
    public CharacterController characterController;
    public Transform body;
    
    [Header("Logic")]
    public Vector3 velocity;
    public float gravity = -9.81f;
    public bool isGrounded;
    public bool isCrouching = false;
    public float groundDistance = 1.2f;
    
    [Header("Movement")]
    public float speed = 7f;
    public float jumpHeight = 5f;
    float inputX , inputZ;
    
    [Header("Camera")]
    public Camera camera;
    public float sensitivity = 400;
    float rotX , rotY;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
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
        
        HandleCameraMovement();

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            HandleJump();
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            HandleDash();
        }
        
    }

    void FixedUpdate()
    {
        HandleGravity();
        
        HandleMovement();
        
        HandleVelocity();
    }

    public void CheckGrounded()
    {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, groundDistance); 
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

    public void HandleMovement()
    {
        if (!Input.GetKey(KeyCode.C))
        {
            velocity.x = (transform.right * inputX + transform.forward * inputZ).x * 4f;
            velocity.z = (transform.right * inputX + transform.forward * inputZ).z * 4f;
        }

        characterController.Move(velocity * Time.deltaTime);
    }
    
    public void HandleJump()
    {
        velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
    }

    public void HandleDash()
    {
        velocity = (transform.right * inputX + transform.forward * inputZ) * 200f;
    }

    public void HandleVelocity()
    {
        velocity.x = Mathf.Lerp(velocity.x, 0, Time.deltaTime);
        velocity.z = Mathf.Lerp(velocity.z, 0, Time.deltaTime);
    }

    
    public void HandleInput()
    { 
        inputX = Input.GetAxis("Horizontal");
        inputZ = Input.GetAxis("Vertical");
    }
    
    public void HandleCameraMovement()
    {
        //Mouse Input
        rotX += Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        rotY -= Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;
        
        //Limit rot
        rotY = Mathf.Clamp(rotY, -90, 90);
        
        //Rotate Camera and Player(Body)
        camera.transform.localRotation = Quaternion.Euler(rotY, 0 , 0);
        
        //body.Rotate(Vector3.up * rotX);
        transform.rotation = Quaternion.Euler(0, rotX, 0);
    }
    
}
