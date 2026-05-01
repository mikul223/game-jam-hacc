using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NPC : MonoBehaviour
{
    [Header("Настройки NPC")]
    public string npcName;
    public Sprite portrait;
    [TextArea(3, 5)]
    public string[] goodDialogs;
    [TextArea(3, 5)]
    public string[] badDialogs;

    [Header("Цвет NPC (эмоция)")]
    public Color goodColor = Color.white;
    public Color badColor = Color.red;

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
        // Открыть диалог по F
        if (playerNear && Input.GetKeyDown(KeyCode.F) && !dialogOpen)
        {
            OpenDialog();
        }

        // Переключение реплик по клику мыши
        if (dialogOpen && Input.GetMouseButtonDown(0))
        {
            NextDialog();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerNear = true;
            promptText.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerNear = false;
            promptText.SetActive(false);
        }
    }

    void OpenDialog()
    {
        dialogOpen = true;
        dialogIndex = 0;

        // Выбор реплик в зависимости от ошибок
        int mistakes = GameManager.instance.mistakes;
        currentDialogs = (mistakes == 0) ? goodDialogs : badDialogs;

        // Показываем UI
        dialogPanel.SetActive(true);
        npcPortrait.sprite = portrait;

        // Показываем первую реплику
        ShowCurrentDialog();

        // Останавливаем игрока
        PlayerMovement player = FindAnyObjectByType<PlayerMovement>();
        if (player != null) player.enabled = false;
    }

    void ShowCurrentDialog()
    {
        if (currentDialogs != null && dialogIndex < currentDialogs.Length)
        {
            dialogText.text = npcName + ": " + currentDialogs[dialogIndex];
        }
    }

    void NextDialog()
    {
        dialogIndex++;

        if (currentDialogs != null && dialogIndex < currentDialogs.Length)
        {
            // Следующая реплика
            ShowCurrentDialog();
        }
        else
        {
            // Реплики закончились — закрыть диалог
            CloseDialog();
        }
    }

    void CloseDialog()
    {
        dialogOpen = false;
        dialogPanel.SetActive(false);

        // Включаем движение игрока
        PlayerMovement player = FindAnyObjectByType<PlayerMovement>();
        if (player != null) player.enabled = true;
    }

    public void UpdateAppearance()
    {
        if (spriteRenderer == null) spriteRenderer = GetComponent<SpriteRenderer>();
        int mistakes = GameManager.instance.mistakes;
        spriteRenderer.color = (mistakes == 0) ? goodColor : badColor;
    }
}