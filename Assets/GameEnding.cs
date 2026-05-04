using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameEnding : MonoBehaviour
{
    public GameObject endingPanel;
    public TextMeshProUGUI endingTitle;
    public TextMeshProUGUI endingText;

    void Start()
    {
        ShowEnding();
    }

    void ShowEnding()
    {
        GameManager gm = GameManager.instance;
        if (gm == null) return;

        bool isGood = gm.totalMistakes <= gm.maxMistakesForGood;

        if (isGood)
        {
            endingTitle.text = "Хорошая концовка";
            endingTitle.color = new Color(0.8f, 1f, 0.8f);
            endingText.text = "Вы справились. Работа выполнена правильно, город живёт своей жизнью, а соседи всё так же приветливо машут вам по утрам.\n\nИногда лучший выбор — это не экспериментировать, а просто делать то, что просят.";
        }
        else
        {
            endingTitle.text = "Плохая концовка";
            endingTitle.color = new Color(1f, 0.6f, 0.6f);
            endingText.text = "Город изменился. Соседи больше не смотрят вам в глаза, а дождь, кажется, уже никогда не закончится.\n\nВы хотели как лучше, но что-то пошло не так. Может, в другой раз стоит быть внимательнее?";
        }

        endingPanel.SetActive(true);
    }
}