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

        //Updates the Money To the quota.
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
