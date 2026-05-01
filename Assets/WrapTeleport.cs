using UnityEngine;

public class WrapTeleport : MonoBehaviour
{
    public Transform targetPoint;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.position = targetPoint.position;
        }
    }
}