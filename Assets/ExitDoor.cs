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

        //Определяем последствия
        GameManager.instance.DetermineMood();

        // Ставим флаг завершения работы
        GameManager.instance.workDoneToday = true;

        //  Обновляем NPC 
        NPC[] allNPCs = FindObjectsByType<NPC>(FindObjectsInactive.Exclude);
        foreach (NPC npc in allNPCs)
        {
            npc.UpdateAppearance();
        }

        PlayerMovement player = FindAnyObjectByType<PlayerMovement>();
        if (player != null)
        {
            player.enabled = true;
            player.transform.position = streetSpawnPoint.position;
        }
    }

    public void ConfirmNo()
    {
        confirmPanel.SetActive(false);
        doorPrompt.SetActive(true);

        PlayerMovement player = FindAnyObjectByType<PlayerMovement>();
        if (player != null) player.enabled = true;
    }
}