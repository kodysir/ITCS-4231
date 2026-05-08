using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class obeseSpawner : MonoBehaviour
{
    public GameObject normalCustomerPrefab;
    public GameObject obeseCustomerPrefab; // Drag your special AI here
    public Transform spawnPoint;
    public float spawnDelay = 5f;
    
    private bool canSpawn = true;

    void Start()
    {
        // Start the spawning cycle
        InvokeRepeating("AttemptSpawn", 2f, spawnDelay);
    }

    void AttemptSpawn()
    {
        if (canSpawn)
        {
            // 20% chance to spawn the Obese AI, 80% for normal
            int currentMoney = MoneyManager.Instance.GetTotalMoney();
            GameObject prefabToSpawn;
            
            if (currentMoney >= 300 && Random.value < 0.2f) 
            {
                prefabToSpawn = obeseCustomerPrefab;
                Debug.Log("A rare Obese AI has appeared!");
            }
            else 
            {
                prefabToSpawn = normalCustomerPrefab;
            }
            Instantiate(prefabToSpawn, spawnPoint.position, spawnPoint.rotation);
            
            // Optional: Set canSpawn to false if you only want 1 customer at a time
            // canSpawn = fals; 
        }
    }

    // Call this from the CustomerAI's "Despawn" logic to allow a new one
    public void SetCanSpawn(bool value)
    {
        canSpawn = value;
    }
}