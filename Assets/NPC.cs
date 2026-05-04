using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NPC : MonoBehaviour
{
    [Header("Имя")]
    public string npcName;

    [Header("Спрайты для каждого настроения")]
    public Sprite normalSprite;
    public Sprite angrySprite;
    public Sprite sadSprite;
    public Sprite happySprite;
    public Sprite scaredSprite;
    

    [Header("Портреты для диалогов")]
    public Sprite normalPortrait;
    public Sprite angryPortrait;
    public Sprite sadPortrait;
    public Sprite happyPortrait;
    public Sprite scaredPortrait;

    [Header("Реплики")]
    [TextArea(2, 4)] public string[] normalDialogs;
    [TextArea(2, 4)] public string[] angryDialogs;
    [TextArea(2, 4)] public string[] sadDialogs;
    [TextArea(2, 4)] public string[] happyDialogs;
    [TextArea(2, 4)] public string[] scaredDialogs;

    [Header("Ссылки на UI")]
    public GameObject promptText;
    public GameObject dialogPanel;
    public Image npcPortrait;
    public TextMeshProUGUI dialogText;

    private SpriteRenderer spriteRenderer;
    private bool playerNear = false;
    private bool dialogOpen = false;
    private string[] currentDialogs;
    private int dialogIndex = 0;
    private GameManager.Mood myMood;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        UpdateAppearance();
    }

    void Update()
    {
        if (playerNear && Input.GetKeyDown(KeyCode.F) && !dialogOpen)
        {
            OpenDialog();
        }

        if (dialogOpen && Input.GetMouseButtonDown(0))
        {
            NextDialog();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && promptText != null)
        {
            playerNear = true;
            promptText.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && promptText != null)
        {
            playerNear = false;
            promptText.SetActive(false);
        }
    }

    void OpenDialog()
    {
        dialogOpen = true;
        dialogIndex = 0;

        currentDialogs = GetDialogsForMood(myMood);
        Sprite portrait = GetPortraitForMood(myMood);

        dialogPanel.SetActive(true);
        npcPortrait.sprite = portrait;

        if (currentDialogs.Length > 0)
            dialogText.text = npcName + ": " + currentDialogs[0];
        else
            dialogText.text = npcName + ": ...";

        PlayerMovement player = FindAnyObjectByType<PlayerMovement>();
        if (player != null) player.enabled = false;
    }

    void NextDialog()
    {
        dialogIndex++;
        if (dialogIndex < currentDialogs.Length)
        {
            dialogText.text = npcName + ": " + currentDialogs[dialogIndex];
        }
        else
        {
            CloseDialog();
        }
    }

    void CloseDialog()
    {
        dialogOpen = false;
        dialogPanel.SetActive(false);

        PlayerMovement player = FindAnyObjectByType<PlayerMovement>();
        if (player != null) player.enabled = true;
    }

    public void UpdateAppearance()
    {
        if (spriteRenderer == null) spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null) return;

        GameManager gm = GameManager.instance;

        // Если сегодня работа не завершена — берём настроение из GM
        if (!gm.workDoneToday)
        {
            // Если был случайный набор — даём случайную эмоцию
            if (gm.wasRandomMood)
            {
                myMood = (GameManager.Mood)Random.Range(1, 5);
            }
            else
            {
                myMood = gm.currentMood;
            }
            spriteRenderer.sprite = GetSpriteForMood(myMood);
            Debug.Log($"{npcName}: день {gm.currentDay}, работа не завершена, настроение = {myMood}");
            return;
        }

        // После работы — проверяем
        FlowerManager fm = FlowerManager.instance;
        
        bool allCorrect = (fm.redCount == fm.redNormal && 
                        fm.blueCount == fm.blueNormal && 
                        fm.yellowCount == fm.yellowNormal && 
                        fm.purpleCount == fm.purpleNormal);

        if (allCorrect)
        {
            myMood = GameManager.Mood.Normal;
        }
        else
        {
            GameManager.Mood gmMood = GameManager.instance.currentMood;
            bool randomMood = GameManager.instance.wasRandomMood;
            
            if (randomMood || gmMood == GameManager.Mood.Normal)
            {
                myMood = (GameManager.Mood)Random.Range(1, 5);
            }
            else
            {
                myMood = gmMood;
            }
        }

        spriteRenderer.sprite = GetSpriteForMood(myMood);
    }


    Sprite GetSpriteForMood(GameManager.Mood mood)
    {
        switch (mood)
        {
            case GameManager.Mood.Angry: return angrySprite;
            case GameManager.Mood.Sad: return sadSprite;
            case GameManager.Mood.Happy: return happySprite;
            case GameManager.Mood.Scared: return scaredSprite;
            default: return normalSprite;
        }
    }

    Sprite GetPortraitForMood(GameManager.Mood mood)
    {
        switch (mood)
        {
            case GameManager.Mood.Angry: return angryPortrait;
            case GameManager.Mood.Sad: return sadPortrait;
            case GameManager.Mood.Happy: return happyPortrait;
            case GameManager.Mood.Scared: return scaredPortrait;
            default: return normalPortrait;
        }
    }

    string[] GetDialogsForMood(GameManager.Mood mood)
    {
        switch (mood)
        {
            case GameManager.Mood.Angry: return angryDialogs;
            case GameManager.Mood.Sad: return sadDialogs;
            case GameManager.Mood.Happy: return happyDialogs;
            case GameManager.Mood.Scared: return scaredDialogs;
            default: return normalDialogs;
        }
    }
}