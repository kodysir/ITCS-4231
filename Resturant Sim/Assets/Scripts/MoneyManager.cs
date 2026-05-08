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

        // Check if the text reference actually exists before trying to use it
        if (moneyText != null)
        {
            moneyText.text = "$" + totalMoney;
        }
        else
        {
            Debug.LogWarning("MoneyManager is missing its moneyText reference in the Inspector!");
        }

        // This part will now run even if the text slot is empty!
        QuotaManager quota = FindObjectOfType<QuotaManager>();
        if (quota != null)
        {
            quota.AddToQuotaProgress(amount);
        }
    
        Debug.Log("Transaction Complete! New Balance: $" + totalMoney);
    }

    public int GetTotalMoney()
    {
        return totalMoney;
    }
}
