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

    public GameObject cantSleepMessage; 

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
        GameManager gm = GameManager.instance;
        if (gm == null) return;

        if (!gm.workDoneToday && gm.currentDay > 1)
        {
            Debug.Log("Нельзя спать — вы ещё не были на работе!");
            if (bedPrompt != null) bedPrompt.SetActive(false);
            ShowCantSleepMessage();
            return;
        }

        sleeping = true;
        sleepPanel.SetActive(true);
        if (bedPrompt != null) bedPrompt.SetActive(false);

        int day = gm.currentDay;
        sleepText.text = "Наступила ночь...\nДень " + day + " завершён.";

        PlayerMovement player = FindAnyObjectByType<PlayerMovement>();
        if (player != null) player.enabled = false;
    }

    void WakeUp()
    {
        sleeping = false;
        sleepPanel.SetActive(false);

        GameManager.instance.NextDay();
        GameManager.instance.workDoneToday = false;

        if (GameManager.instance.currentDay > GameManager.instance.totalDays)
        {
            Canvas canvas = FindAnyObjectByType<Canvas>();
            GameEnding ending = canvas.GetComponent<GameEnding>();
            if (ending != null) ending.ShowEnding();
            return;
        }

        Canvas canvas2 = FindAnyObjectByType<Canvas>();
        DayIntro intro = canvas2.GetComponent<DayIntro>();
        if (intro != null) intro.ShowDayIntro(GameManager.instance.currentDay);

        if (FlowerManager.instance != null)
            FlowerManager.instance.ResetCounts();

        NPC[] allNPCs = FindObjectsByType<NPC>(FindObjectsInactive.Exclude);
        foreach (NPC npc in allNPCs)
            npc.UpdateAppearance();

        PlayerMovement player = FindAnyObjectByType<PlayerMovement>();
        if (player != null)
        {
            player.enabled = true;
            player.transform.position = transform.position + Vector3.left * 2f;
        }
    }

    void ShowCantSleepMessage()
    {
        if (cantSleepMessage != null)
        {
            cantSleepMessage.SetActive(true);
            Invoke("HideCantSleepMessage", 2f);
        }
    }

    void HideCantSleepMessage()
    {
        if (cantSleepMessage != null)
            cantSleepMessage.SetActive(false);
    }
}