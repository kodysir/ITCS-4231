using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class obeseAI : CustomerAI
{
    public override void GenerateOrder() 
    {
        int amountToOrder = Random.Range(3, 6); // Obese AI orders 3-5 items
        for (int i = 0; i < amountToOrder; i++)
        {
            string randomItem = menu[Random.Range(0, menu.Length)];
            orderList.Add(randomItem);
        }
        orderTextDisplay.text = "MEGA ORDER:\n" + string.Join("\n", orderList);
    }
}