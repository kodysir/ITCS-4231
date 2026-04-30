using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderButton : MonoBehaviour
{
    public OrderStand stand;
    public CustomerAI activeCustomer;


    public void SetActiveCustomer(CustomerAI customer)
    {
        activeCustomer = customer;
        Debug.Log("Button is now locked onto: " + customer.name);
    }
    public void SubmitOrder()
    {
        //Checks if the current customer is at the order window
        Debug.Log("Submit button clicked!");
        activeCustomer = FindObjectOfType<CustomerAI>();

        if(activeCustomer == null)
        {
            Debug.Log("Button: No customer found in scene!");
            return;
        }

        if(stand.foodInZone.Count == 0)
        {
            Debug.Log("Button: The stand is empty!");
            return;
        }
        
        bool isCorrect = false;
        string neededFood = activeCustomer.GetOrder();
        int payout = 0;

        int basePrice = 0;
        if (neededFood == "Grilled Cheese")
        {
            basePrice = 4;
        }else if (neededFood == "Hamburger")
        {
            basePrice = 6;
        }else if (neededFood == "Cheese Burger")
        {
            basePrice = 7;
        }

        foreach (GameObject food in stand.foodInZone)
        {
            if (food.name.Contains(neededFood))
            {
                isCorrect = true;
                break;
            }
        }

        if (isCorrect)
        {
            payout = basePrice;
        }
        else
        {
            payout = 3;
        }

        MoneyManager.Instance.AddMoney(payout);
        activeCustomer.Leave(isCorrect);
        stand.ClearCounter();
    }
}
