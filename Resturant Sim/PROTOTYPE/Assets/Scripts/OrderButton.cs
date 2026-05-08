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
        Debug.Log("Submit button clicked!");

        // FIXED: Removed FindObjectOfType. We use the activeCustomer set by the E interaction.
        if (activeCustomer == null)
        {
            Debug.Log("Button: No active customer! You need to talk to one first.");
            return;
        }

        if (stand.foodInZone.Count == 0)
        {
            Debug.Log("Button: The stand is empty!");
            return;
        }

        // 1. Get the full list of items they want
        List<string> neededOrders = activeCustomer.GetOrderList();
        int totalPayout = 0;
        bool everythingCorrect = true;

        // 2. Create a temporary list of what's on the stand 
        // This prevents one burger from counting for two different orders
        List<GameObject> foodRemaining = new List<GameObject>(stand.foodInZone);

        // 3. Loop through the customer's grocery list
        foreach (string itemNeeded in neededOrders)
        {
            bool foundThisItem = false;

            for (int i = 0; i < foodRemaining.Count; i++)
            {
                if (foodRemaining[i].name.Contains(itemNeeded))
                {
                    // Add the specific price based on your note card
                    totalPayout += GetPrice(itemNeeded);
                    
                    // "Consume" the food item so it's not checked again
                    foodRemaining.RemoveAt(i);
                    foundThisItem = true;
                    break;
                }
            }

            if (!foundThisItem)
            {
                // If they didn't get an item they asked for, give $3 consolation
                totalPayout += 3;
                everythingCorrect = false;
            }
        }

        // 4. Finalize the transaction
        MoneyManager.Instance.AddMoney(totalPayout);
        
        // 5. Send the AI on their way
        activeCustomer.Leave(everythingCorrect);
        
        // 6. Reset for the next customer
        stand.ClearCounter();
        activeCustomer = null; 
    }

    // A helper method to keep the main logic clean and organized
    private int GetPrice(string itemName)
    {
        if (itemName == "Grilled Cheese") return 4;
        if (itemName == "Hamburger") return 6;
        if (itemName == "Cheese Burger") return 7;
        return 0;
    }
}