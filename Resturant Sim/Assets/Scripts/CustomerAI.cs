using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;
using Unity.VisualScripting;
using Cinemachine;
using UnityEditor;

public class CustomerAI : MonoBehaviour
{
    [Header("References")]
    public NavMeshAgent agent;
    public Transform orderWindow; // Drag the "Window" target here
    public GameObject canvasUI;   // Drag the "Canvas" child here
    public TextMeshProUGUI orderTextDisplay;
    public Transform exitPoint;

    [Header("Order Settings")]
    public List<string> orderList = new List<string>();
    public string[] menu = {"Grilled Cheese", "Hamburger", "Cheese Burger" };

    [Header("Amount")]
    public int minItems = 1;
    public int maxItems = 1;

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
        GenerateOrder();
    }

    public virtual void GenerateOrder()
    {
        orderList.Clear();
        int amountToOrder = Random.Range(minItems, maxItems);

        for(int i = 0; i < amountToOrder; i++)
        {
            int orderVal = Random.Range(0, menu.Length);
            string selectedFood = menu[orderVal];
            orderList.Add(selectedFood);
            Debug.Log("Rolled a " + orderVal + " which is " + selectedFood);
        }

        
    }

    public void StartOrder()
    {
        // Only show order if the customer is close to the window
        if (agent.remainingDistance <= 1.0f)
        {
            orderTextDisplay.text = string.Join("\n", orderList);
            canvasUI.SetActive(true);
            OrderButton theButton = FindObjectOfType<OrderButton>();
            if (theButton != null)
            {
                theButton.SetActiveCustomer(this);
            }
            Debug.Log("Customer ordered: " + orderList);
        }
    }

    public List<string> GetOrderList()
    {
        return orderList;
    }

    public void Leave(bool isCorrect)
    {
        if (isCorrect) {
            orderTextDisplay.text = "Delicious! Thanks!";
        } else {
            orderTextDisplay.text = "This isn't my order.";
        }

        if (exitPoint != null)
        {
            //Go to the exit
            agent.SetDestination(exitPoint.position);
        }

        //Check if the customer reaches the exit
        InvokeRepeating("CheckIfReachedExit", 1f, 1f);
    }

    void CheckIfReachedExit()
    {
        //If the distance is close to 0
        if (agent.remainingDistance <= 0.5f && !agent.pathPending)
        {
            Destroy(gameObject); // Despawns the customer
        }
    }
}
