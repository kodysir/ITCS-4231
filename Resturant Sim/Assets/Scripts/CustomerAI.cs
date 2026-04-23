using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;

public class CustomerAI : MonoBehaviour
{
    [Header("References")]
    public NavMeshAgent agent;
    public Transform orderWindow; // Drag the "Window" target here
    public GameObject canvasUI;   // Drag the "Canvas" child here
    public TextMeshProUGUI orderTextDisplay;

    [Header("Order Settings")]
    private string currentOrder;
    private string[] menu = {"Grilled Cheese", "Hamburger", "Cheese Burger" };

    // Start is called before the first frame update
    void Start()
    {
        // 1. Hide the order UI initially
        canvasUI.SetActive(false);

        // 2. Tell the AI to walk to the window immediately
        if (orderWindow != null)
        {
            agent.SetDestination(orderWindow.position);
        }

        // 3. Pre-roll the random order (hidden from player)
        int randomIndex = Random.Range(0, menu.Length);
        currentOrder = menu[randomIndex];
    }

    public void StartOrder()
    {
        // Only show order if the customer is close to the window
        if (agent.remainingDistance <= 1.0f)
        {
            orderTextDisplay.text = currentOrder;
            canvasUI.SetActive(true);
            Debug.Log("Customer ordered: " + currentOrder);
        }
    }

    public string GetOrder()
    {
        return currentOrder;
    }
}
