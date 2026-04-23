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
            Rigidbody rigidbody = other.GetComponent<Rigidbody>();
            if (rigidbody != null)
            {
                rigidbody.WakeUp();
            }

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
            foodInZone.Remove(other.gameObject);
            Debug.Log(other.name + " is not on the counter");
        }
    }

}