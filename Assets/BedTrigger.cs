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
        {
            playerNear = true;
            if (bedPrompt != null) bedPrompt.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerNear = false;
            if (bedPrompt != null) bedPrompt.SetActive(false);
        }
    }

    void GoToSleep()
    {
        sleeping = true;
        sleepPanel.SetActive(true);
        if (bedPrompt != null) bedPrompt.SetActive(false);

        GameManager gm = GameManager.instance;
        if (gm == null) return;

        int day = gm.currentDay;
        int mistakes = gm.totalMistakes;

        sleepText.text = "Наступила ночь...\nДень " + day + " завершён.";

        if (mistakes == 0)
            sleepHint.text = "Сегодня всё прошло хорошо. Кликните, чтобы проснуться.";
        else if (mistakes <= gm.maxMistakesForGood)
            sleepHint.text = "Вы ошиблись, но завтра всё можно исправить. Кликните, чтобы проснуться.";
        else
            sleepHint.text = "Всё пошло не так... Кликните, чтобы проснуться.";

        PlayerMovement player = FindAnyObjectByType<PlayerMovement>();
        if (player != null) player.enabled = false;
    }

    void WakeUp()
    {
        sleeping = false;
        sleepPanel.SetActive(false);

        // Следующий день (меняет currentMood и isRaining внутри)
        GameManager.instance.NextDay();
        GameManager.instance.workDoneToday = false;

        // Сброс счётчиков цветов
        if (FlowerManager.instance != null)
            FlowerManager.instance.ResetCounts();

        // Обновить NPC (использует currentMood из GameManager)
        NPC[] allNPCs = FindObjectsByType<NPC>(FindObjectsInactive.Exclude);
        foreach (NPC npc in allNPCs)
        {
            npc.UpdateAppearance();
        }

        PlayerMovement player = FindAnyObjectByType<PlayerMovement>();
        if (player != null)
        {
            player.enabled = true;
            player.transform.position = transform.position + Vector3.left * 2f;
        }
    }
}