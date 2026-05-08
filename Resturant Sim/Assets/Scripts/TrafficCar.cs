using UnityEngine;

public class TrafficCar : MonoBehaviour
{
    public Transform startPoint;
    public Transform endPoint;
    public float speed = 8f;

    void Update()
    {
        transform.position = Vector3.MoveTowards(
            transform.position,
            endPoint.position,
            speed * Time.deltaTime
        );

        if (Vector3.Distance(transform.position, endPoint.position) < 0.2f)
        {
            transform.position = startPoint.position;
        }
    }
}