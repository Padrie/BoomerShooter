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
    public bool dashing;
    public bool canDash = true;
    public bool bufferJump;
    public float groundDistance = 1.2f;
    
    [Header("Movement")]
    public float speed = 4f;
    public float airSpeed = 6f;
    public float jumpHeight = 5f;
    public float airDashTime = 40f ,airDashSpeed = 50f;
    public float dashTime = 40f ,dashSpeed = 75f;
    public float inputX , inputZ;
    
    [Header("Camera")]
    public Camera camera;
    public float sensitivity = 400;
    public float cameraTilt = 2.5f, tiltSpeed = 2;
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

        /*if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            HandleJump();
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            StartCoroutine(HandleDash());
        }*/
        
    }

    void FixedUpdate()
    {
        HandleGravity();
        
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
        if (isGrounded)
        {
            velocity.x = (transform.right * inputX + transform.forward * inputZ).x * speed;
            velocity.z = (transform.right * inputX + transform.forward * inputZ).z * speed;
        }
        else
        {
            velocity.x += (transform.right * inputX + transform.forward * inputZ).x * airSpeed * 0.02f;
            velocity.z += (transform.right * inputX + transform.forward * inputZ).z * airSpeed * 0.02f;
        }

        characterController.Move(velocity * Time.deltaTime);
    }
    
    public void HandleJump()
    {
        velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
    }

    public IEnumerator HandleAirDash()
    {
        dashing = true;
        for (int i = 0; i < airDashTime; i++)
        {
            velocity = (transform.right * inputX + transform.forward * inputZ) * airDashSpeed;
            Debug.Log(i);
            if (i == airDashTime - 1)
            {
                dashing = false;
                Debug.Log("Done");
            }
            yield return null;
        }
    }
    
    public IEnumerator HandleDash()
    {
        dashing = true;
        for (int i = 0; i < dashTime; i++)
        {
            velocity = (transform.right * inputX + transform.forward * inputZ) * dashSpeed;

            if (i == dashTime - 1)
            {
                dashing = false;
                Debug.Log("Done");
            }
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
