using System;
using System.Collections;
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
    public float groundDistance = 1.2f;
    
    [Header("Movement")]
    public float speed = 4f;
    public float airSpeed = 6f;
    public float jumpHeight = 5f;
    public float dashTime = 20f;
    public float dashSpeed = 50f;
    float inputX , inputZ;
    
    [Header("Camera")]
    public Camera camera;
    public float sensitivity = 400;
    public float cameraTilt = 3, tiltSpeed = 2;
    float rotX , rotY, rotZ;
    
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
            StartCoroutine(HandleDash());
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
        if (inputX != 0 || inputZ != 0)
        {
            if (isGrounded)
            {
                velocity.x = (transform.right * inputX + transform.forward * inputZ).x * speed;
                velocity.z = (transform.right * inputX + transform.forward * inputZ).z * speed;
            }
            else
            {
                velocity.x = (transform.right * inputX + transform.forward * inputZ).x * airSpeed;
                velocity.z = (transform.right * inputX + transform.forward * inputZ).z * airSpeed;
            }
            
        }

        characterController.Move(velocity * Time.deltaTime);
    }
    
    public void HandleJump()
    {
        velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
    }

    public IEnumerator HandleDash()
    {
        for (int i = 0; i < dashTime; i++)
        {
            velocity = (transform.right * inputX + transform.forward * inputZ) * dashSpeed;
            yield return null;
        }
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
        rotZ = Mathf.Lerp(rotZ, -inputX * cameraTilt, Time.deltaTime * tiltSpeed);
        
        //Rotate Camera and Player(Body)
        camera.transform.localRotation = Quaternion.Euler(rotY, 0 , rotZ);
        
        //body.Rotate(Vector3.up * rotX);
        transform.rotation = Quaternion.Euler(0, rotX, 0);
    }
    
}
