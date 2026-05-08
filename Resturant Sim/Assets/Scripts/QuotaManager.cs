using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuotaManager : MonoBehaviour
{
    public TextMeshPro quotaText;
    public GameObject continueMenu;

    private int currentQuota = 10;
    private int quotaIncrement = 110;
    private int maxQuota = 1000;
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
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Log("Quitting Game");
            UnityEditor.EditorApplication.isPlaying = false;
            Application.Quit();
        }
        // Always update the text: "Current / Target"
        quotaText.text = "Quota: $" + totalEarnedThisRound + " / $" + currentQuota;

        // Check if we hit the goal and haven't shown the menu yet
        if (totalEarnedThisRound >= currentQuota && !quotaReached)
        {
            ReachQuota();
        }

        if(totalEarnedThisRound >= 1000)
        {
            SceneManager.LoadScene("WinScreen");
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
        Debug.Log("Quota reached! Moving to next round automatically.");

        // Instead of showing the menu, just call the logic directly
        ContinueToNextRound(); 
    }

    public void ContinueToNextRound()
    {
        totalEarnedThisRound = 0;
        quotaReached = false;

        if (currentQuota < maxQuota)
        {
            currentQuota += quotaIncrement; 
        
            // Only update the wall text if it actually exists
            if (quotaText != null)
            {
                quotaText.text = "Quota: $" + totalEarnedThisRound + " / $" + currentQuota;
            }
        }
    
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        if(continueMenu != null)
        {
            continueMenu.SetActive(false);
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

