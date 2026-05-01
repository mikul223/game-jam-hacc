using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BedTrigger : MonoBehaviour
{
    [Header("UI")]
    public GameObject sleepPanel;
    public TextMeshProUGUI sleepText;
    public TextMeshProUGUI sleepHint;
    public GameObject bedPrompt;


    private bool playerNear = false;
    private bool sleeping = false;

    void Update()
    {
        if (playerNear && Input.GetKeyDown(KeyCode.F) && !sleeping)
        {
            GoToSleep();
        }

        if (sleeping && Input.GetMouseButtonDown(0))
        {
            WakeUp();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            playerNear = true;
            bedPrompt.SetActive(true);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            playerNear = false;
            bedPrompt.SetActive(false);
    }

    void GoToSleep()
    {
        sleeping = true;
        sleepPanel.SetActive(true);
        bedPrompt.SetActive(false);

        int day = GameManager.instance.currentDay;
        int mistakes = GameManager.instance.mistakes;

        sleepText.text = "Наступила ночь...\nДень " + day + " завершён.";

        if (mistakes == 0)
            sleepHint.text = "Сегодня всё прошло хорошо. Кликните, чтобы проснуться.";
        else if (mistakes <= GameManager.instance.maxMistakes)
            sleepHint.text = "Вы ошиблись, но завтра всё можно исправить. Кликните, чтобы проснуться.";
        else
            sleepHint.text = "Всё пошло не так... Кликните, чтобы проснуться.";

        // Остановить игрока
        PlayerMovement player = FindAnyObjectByType<PlayerMovement>();
        if (player != null) player.enabled = false;
    }

    void WakeUp()
    {
        sleeping = false;
        sleepPanel.SetActive(false);
        bedPrompt.SetActive(true);

        // Переход на следующий день
        GameManager.instance.NextDay();
        GameManager.instance.workDoneToday = false;

        // Показать, что начался новый день
        Debug.Log("День " + GameManager.instance.currentDay + " начался!");

        // Обновить всех NPC
        NPC[] allNPCs = FindObjectsByType<NPC>(FindObjectsSortMode.None);
        foreach (NPC npc in allNPCs)
        {
            npc.UpdateAppearance();
        }

        // Включить игрока
        PlayerMovement player = FindAnyObjectByType<PlayerMovement>();
        if (player != null) player.enabled = true;

        // Телепортировать игрока к началу локации (выход из дома)
        GameObject spawn = GameObject.Find("SpawnPointHome");
        if (spawn != null && player != null)
        {
            player.transform.position = spawn.transform.position;
        }
    }
}