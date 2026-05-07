using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("References")]
    private CharacterController controller;
    [SerializeField] private Transform playerCam; //Eyes
    [SerializeField] private Transform Hand; // Hand

    [Header("Interaction Settings")]
    [SerializeField] private float reachDistance = 6f;
    [SerializeField] private KeyCode interactKey = KeyCode.E;
    private GameObject heldObj;
   

    [Header("Movement Settings")]
    [SerializeField] private float walkSpeed = 5f;
    [SerializeField] private float sprintSpeed = 9f;

    [Header("Jump & Gravity")]
    public float jumpHeight = 1.5f;
    public float gravity = -9.81f;
    private Vector3 playerVelocity;

    [Header("Input")]
    private float moveInput;
    private float turnInput;

    [Header("1st Person Camera Settings")]
    [SerializeField] private float sensitivity = 100f;
    private float xRotation = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        ManageInput();
        Movement();

        if (Input.GetKeyDown(interactKey))
        {
            HandleInteraction();
        }
    }

    private void Movement()
    {
        //ApplyGravityAndJump & GroundMovement
        bool isGrounded = controller.isGrounded;

        if (isGrounded && playerVelocity.y < 0)
            playerVelocity.y = -2f;

        if (Input.GetButtonDown("Jump")  && isGrounded)
        {
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        playerVelocity.y += gravity * Time.deltaTime;

        //MovementSpeed
        float currentSpeed = Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : walkSpeed;

        //HorizontalMovement
        Vector3 move = transform.forward * moveInput + transform.right * turnInput;
        move = Vector3.ClampMagnitude(move, 1f);

        Vector3 finalMove = move * currentSpeed;
        finalMove.y = playerVelocity.y;

        controller.Move(finalMove * Time.deltaTime);
    }


    private void ManageInput()
    {
        moveInput = Input.GetAxis("Vertical");
        turnInput = Input.GetAxis("Horizontal");

        float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        playerCam.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        transform.Rotate(Vector3.up * mouseX);
    }

    private void HandleInteraction()
    {
        if (heldObj == null)
        {
            Vector3 rayStart = playerCam.position + (playerCam.forward * 0.5f);
            RaycastHit hit;

            //Using a Spherecast, the Raycast was too thin
            if (Physics.SphereCast(rayStart, 0.2f, playerCam.forward, out hit, reachDistance))
            {
                //Interact with Food
                if (hit.transform.CompareTag("pickup"))
                {
                    PickUpObject(hit.transform.gameObject);
                }

                //Interact with Customer
                else if (hit.transform.CompareTag("Customer"))
                {
                    Debug.Log("Hit the Customer!");
                    CustomerAI customer = hit.transform.GetComponent<CustomerAI>();
                    
                    if (customer != null)
                    {
                        Debug.Log("Found the Script! Starting Order...");
                        customer.StartOrder();
                    }
                    else
                    {
                        Debug.LogError("Found Customer, but NO CustomerAI script on it!");
                    }
                }
                else if (hit.transform.CompareTag("Button"))
                {
                    Debug.Log("I am looking at the Button!");
                    OrderButton hitButton = hit.transform.GetComponent<OrderButton>();

                    if(hitButton != null)
                    {
                        Debug.Log("Found the script, calling Submit...");
                        hitButton.SubmitOrder();
                    }
                }
            }
        }
        else
        {
            DropObject();
        }
    }

    private void PickUpObject(GameObject obj)
    {

        heldObj = obj;

        OrderStand standScript = FindObjectOfType<OrderStand>();
        if (standScript != null && standScript.foodInZone.Contains(obj))
        {
            standScript.foodInZone.Remove(obj);
            Debug.Log("Manually removed " + obj.name + " from list upon pickup.");
        }
        
        heldObj.transform.SetParent(Hand);
        heldObj.transform.localPosition = Vector3.zero;
        heldObj.transform.localRotation = Quaternion.identity;

        Rigidbody rb = heldObj.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true;
        }
    }

    private void DropObject()
    {
        heldObj.transform.SetParent(null);

        Rigidbody rb = heldObj.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = false;
            rb.WakeUp();
        }

        heldObj = null;
    }
    
}