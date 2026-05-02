using UnityEngine;

public class ExitDoor : MonoBehaviour
{
    public GameObject doorPrompt;
    public GameObject confirmPanel;
    public Transform streetSpawnPoint;

    

    private bool playerNear = false;

    void Update()
    {
        if (playerNear && Input.GetKeyDown(KeyCode.F))
        {
            ShowConfirm();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && doorPrompt != null)
        {
            playerNear = true;
            doorPrompt.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && doorPrompt != null)
        {
            playerNear = false;
            doorPrompt.SetActive(false);
        }
    }

    void ShowConfirm()
    {
        doorPrompt.SetActive(false);
        confirmPanel.SetActive(true);

        PlayerMovement player = FindAnyObjectByType<PlayerMovement>();
        if (player != null) player.enabled = false;
    }

    public void ConfirmYes()
    {
        confirmPanel.SetActive(false);

        GameManager.instance.DetermineMood();

        PlayerMovement player = FindAnyObjectByType<PlayerMovement>();
        if (player != null)
        {
            player.enabled = true;
            player.transform.position = streetSpawnPoint.position;
        }

        GameManager.instance.workDoneToday = true;
    }

    public void ConfirmNo()
    {
        confirmPanel.SetActive(false);
        doorPrompt.SetActive(true);

        PlayerMovement player = FindAnyObjectByType<PlayerMovement>();
        if (player != null) player.enabled = true;
    }
}