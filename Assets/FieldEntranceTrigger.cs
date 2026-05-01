using UnityEngine;

public class FieldEntranceTrigger : MonoBehaviour
{
    public Transform fieldSpawnPoint;
    public GameObject blockedMessage;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        if (GameManager.instance.workDoneToday)
        {
            // День завершён — показать сообщение
            if (blockedMessage != null)
            {
                blockedMessage.SetActive(true);
                Invoke("HideMessage", 2f);
            }
        }
        else
        {
            // Телепорт в поле
            other.transform.position = fieldSpawnPoint.position;
        }
    }

    void HideMessage()
    {
        if (blockedMessage != null)
            blockedMessage.SetActive(false);
    }
}