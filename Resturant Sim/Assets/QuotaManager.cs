using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuotaManager : MonoBehaviour
{
    public TextMeshPro quotaText;
    public GameObject continueMenu;

    private int currentQuota = 50;
    private int quotaIncrement = 100;
    private int maxQuota = 950;
    private int totalEarnedThisRound = 0;
    private bool quotaReached = false;

    [Header("End ShifT")]
    public GameObject summaryMenu;
    public TextMeshProUGUI collectionText;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Always update the text: "Current / Target"
        quotaText.text = "Quota: $" + totalEarnedThisRound + " / $" + currentQuota;

        // Check if we hit the goal and haven't shown the menu yet
        if (totalEarnedThisRound >= currentQuota && !quotaReached)
        {
            ReachQuota();
        }
    }

    // Call this from MoneyManager whenever money is added
    public void AddToQuotaProgress(int amount)
    {
        totalEarnedThisRound += amount;
    }

    void ReachQuota()
    {
        quotaReached = true;
        Time.timeScale = 0f; // Optional: Pause the game while they decide
        continueMenu.SetActive(true);
        Cursor.lockState = CursorLockMode.None; // Make sure they can click the button
        Cursor.visible = true;
    }

    public void ContinueToNextRound()
    {
        totalEarnedThisRound = 0;
        quotaReached = false;

        if (currentQuota < maxQuota)
        {
            currentQuota += quotaIncrement;
            
            // Resume the game
            continueMenu.SetActive(false);
            Time.timeScale = 1f; 
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            
            
            Debug.Log("Next Round Started! New Quota: $" + currentQuota);
        }
        else
        {
            Debug.Log("Final Quota Reached! You Win!");
            // You could load a win screen here
            SceneManager.LoadScene("WinScreen");
        }
    }

    public void EndShiftEarly()
    {
        continueMenu.SetActive(false);
        Time.timeScale = 0f;
        collectionText.text = "Shift Ended! You collected: $" + totalEarnedThisRound;

        summaryMenu.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        Debug.Log("Player ended shift early with: $" + totalEarnedThisRound);
    }
    
    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}
