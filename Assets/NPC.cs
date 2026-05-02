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

        GameManager.Mood mood = GameManager.instance.currentMood;
        currentDialogs = GetDialogsForMood(mood);
        Sprite portrait = GetPortraitForMood(mood);

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

        GameManager.Mood mood = GameManager.instance.currentMood;
        spriteRenderer.sprite = GetSpriteForMood(mood);
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