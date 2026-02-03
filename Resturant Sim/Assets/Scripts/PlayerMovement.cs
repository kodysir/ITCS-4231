using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("References")]
    private CharacterController controller; // Uses the Character Controller Component
    [SerializeField] private Transform playerCam;

    [Header("Movement Settings")]
    [SerializeField] private float walkSpeed = 5f;
    //[SerializeField] private float turnSpeed = 2f;


    [Header("Input")]
    private float moveInput;
    private float turnInput;

    [Header("1st Person Camera Settings")]
    [SerializeField] private float sensitivity = 100f;
    private float xRotation = 0f;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // Locks cursor to screen
        Cursor.visible = false; // Makes Cursor Invisible
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
       ManageInput();
       Movement();
    }

    private void Movement()
    {
        GroundMovement();
    }

    private void GroundMovement()
    {
        // Handle Movement while on the ground
        Vector3 move = transform.forward * moveInput + transform.right * turnInput;
        
        move.y = -1f; // Prevents player from floating off the ground
        
        controller.Move(move * walkSpeed* Time.deltaTime); // Makes movement not depend on frame rate
    }

    private void ManageInput()
    {
        // Handles Inputs for Movement and Turning
        moveInput = Input.GetAxis("Vertical");
        turnInput = Input.GetAxis("Horizontal");

        //Get Mouse Input
        float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

        //Rotates Camera Up & Down 
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        playerCam.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        //Rotates Player Left & Right
        transform.Rotate(Vector3.up * mouseX);
    }

   
}
