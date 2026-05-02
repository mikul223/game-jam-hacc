using UnityEngine;

public class SignBoard : MonoBehaviour
{
    public GameObject signPrompt;
    public GameObject taskImage;

    private bool playerNear = false;

    void Update()
    {
        if (playerNear && Input.GetKeyDown(KeyCode.F))
        {
            OpenTask();
        }

        if (taskImage.activeSelf && Input.GetMouseButtonDown(0))
        {
            CloseTask();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && signPrompt != null)
        {
            playerNear = true;
            signPrompt.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && signPrompt != null)
        {
            playerNear = false;
            signPrompt.SetActive(false);
        }
    }

    void OpenTask()
    {
        taskImage.SetActive(true);
        signPrompt.SetActive(false);

        PlayerMovement player = FindAnyObjectByType<PlayerMovement>();
        if (player != null) player.enabled = false;
    }

    void CloseTask()
    {
        taskImage.SetActive(false);

        PlayerMovement player = FindAnyObjectByType<PlayerMovement>();
        if (player != null) player.enabled = true;
    }
}