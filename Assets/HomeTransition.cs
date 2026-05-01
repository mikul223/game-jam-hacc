using UnityEngine;

public class LevelTransition : MonoBehaviour
{
    public Transform targetPoint;   // куда телепортировать игрока

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.position = targetPoint.position;
        }
    }
}