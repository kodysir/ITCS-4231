using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderStand : MonoBehaviour
{
    public List<GameObject> foodInZone = new List<GameObject>();

    void Update()
    {
        Debug.Log("Objects in zone: " + foodInZone.Count);
    }

    private void OnTriggerEnter(Collider other)
    {
       if (other.CompareTag("pickup"))
        {
            if (!foodInZone.Contains(other.gameObject))
            {
                foodInZone.Add(other.gameObject);
                Debug.Log(other.name + " is on the counter");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("pickup"))
        {
            if (foodInZone.Contains(other.gameObject))
            {
                foodInZone.Remove(other.gameObject);
                Debug.Log(other.name + " is not on the counter");
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("pickup"))
        {
            if (!foodInZone.Contains(other.gameObject))
            {
                foodInZone.Add(other.gameObject);
                Debug.Log(other.name + " is added to the counter");
            }
        }
    }

    public void ClearCounter()
    {
        foreach (GameObject food in foodInZone)
        {
            Destroy(food);
        }
        foodInZone.Clear();
    }

}