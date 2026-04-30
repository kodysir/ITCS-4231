using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    public static MoneyManager Instance; 
    public TextMeshProUGUI moneyText;
    private int totalMoney = 0;

    void Awake()
    {
        Instance = this;
    }

    public void AddMoney(int amount)
    {
        totalMoney += amount;
        moneyText.text = "$" + totalMoney.ToString();
        Debug.Log("Transaction Complete! New Balance: $" + totalMoney);
    }
}
