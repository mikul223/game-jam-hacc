using UnityEngine;
using TMPro;

public class DayIntro : MonoBehaviour
{
    public GameObject introPanel;
    public TextMeshProUGUI dayTitle;
    public TextMeshProUGUI introText;

    private int currentTextIndex = 0;
    private string[] currentTexts;
    private bool isIntroActive = false;

    public void ShowDayIntro(int day)
    {
        isIntroActive = true;
        introPanel.SetActive(true);

        dayTitle.text = "День " + day;

        GameManager gm = GameManager.instance;
        int mistakes = gm != null ? gm.totalMistakes : 0;

        currentTexts = GetDayTexts(day, mistakes);
        currentTextIndex = 0;

        if (currentTexts.Length > 0)
            introText.text = currentTexts[0];
        else
            introText.text = "";

        PlayerMovement player = FindAnyObjectByType<PlayerMovement>();
        if (player != null) player.enabled = false;
    }

    void Update()
    {
        if (isIntroActive && Input.GetMouseButtonDown(0))
        {
            currentTextIndex++;
            if (currentTextIndex < currentTexts.Length)
            {
                introText.text = currentTexts[currentTextIndex];
            }
            else
            {
                CloseIntro();
            }
        }
    }

    void CloseIntro()
    {
        isIntroActive = false;
        introPanel.SetActive(false);

        PlayerMovement player = FindAnyObjectByType<PlayerMovement>();
        if (player != null) player.enabled = true;
    }

    string[] GetDayTexts(int day, int mistakes)
    {
        switch (day)
        {
            case 1:
                return new string[]
                {
                    "Меня зовут Вэй. Я работаю на космической станции — собираю драгоценные камни среди звёзд.",
                    "Со мной живёт Рраф — мой пёс лавандового раскраса. Он всегда рядом, куда бы я ни шла.",
                    "Сегодня мой первый день. Нужно собрать кристаллы. По одному каждого цвета. Ничего сложного."
                };

            case 2:
                if (mistakes == 0)
                    return new string[]
                    {
                        "Первый день прошёл отлично. Все кристаллы собраны правильно.",
                        "Но, может, стоит попробовать что-то новое? Один лишний камень — что может случиться?",
                        "Рраф виляет хвостом. Ему всё равно на камни. Он просто рад быть рядом.",
                        "Ладно, пора на станцию."
                    };
                else
                    return new string[]
                    {
                        "Вчера я ошиблась. Соседи были... другими.",
                        "Но сегодня всё можно исправить. Нужно просто собрать по одному кристаллу.",
                        "Рраф смотрит на меня с надеждой. Он верит, что всё будет хорошо.",
                        "Я постараюсь. Пора идти."
                    };

            case 3:
                if (mistakes == 0)
                    return new string[]
                    {
                        "Два идеальных дня. Соседи счастливы. Всё стабильно.",
                        "Но разве стабильность — это не скучно? Может, добавить сияния в этот серый мир?",
                        "Один лишний кристалл. Всего один. Никто и не заметит."
                    };
                else if (mistakes == 1)
                    return new string[]
                    {
                        "Вчера всё вернулось в норму. Соседи снова приветливы.",
                        "Но надолго ли? Если я снова ошибусь...",
                        "Рраф спит у моих ног. Ему всё равно. А мне страшно.",
                        "Пора на станцию. Нужно быть осторожнее."
                    };
                else
                    return new string[]
                    {
                        "Две ошибки. Город изменился.",
                        "Соседи больше не те, что раньше. Вернутся ли они когда-нибудь?",
                        "Рраф скулит. Он тоже чувствует, что что-то не так.",
                        "Может, ещё не поздно?"
                    };

            case 4:
                if (mistakes == 0)
                    return new string[]
                    {
                        "Всё ещё идеально. Всё ещё правильно. Всё ещё... однообразно.",
                        "А что, если фиолетовый кристалл? Говорят, он излучает холод. Интересно, какой он — страх?",
                        "Рраф лает. Он против. Но я сама решаю.",
                        "Четвёртый день. Может, пора что-то изменить?"
                    };
                else if (mistakes <= 1)
                    return new string[]
                    {
                        "Новый день. Соседи пока со мной. Но доверие хрупкое.",
                        "Ещё одна ошибка, и я увижу настоящий гнев. Или страх. Или что похуже.",
                        "Рраф трётся о ноги. Он всегда рядом, что бы ни случилось.",
                        "Пора на станцию. Сегодня без ошибок. Обещаю."
                    };
                else
                    return new string[]
                    {
                        "Город тонет в серости.",
                        "Я уже не помню, какими были соседи раньше. Кристаллы молчат. Это пугает.",
                        "Рраф — единственный, кто остался прежним. Но надолго ли?",
                        "Четвёртый день. Я просто хочу, чтобы всё закончилось."
                    };

            case 5:
                if (mistakes == 0)
                    return new string[]
                    {
                        "Четыре идеальных дня позади.",
                        "Я так и не попробовала ничего нового. Может, сегодня? Последний шанс.",
                        "Рраф гавкнул. Кажется, он говорит: 'Не надо'.",
                        "Но это мой выбор. Всего один день."
                    };
                else if (mistakes <= 1)
                    return new string[]
                    {
                        "Одна ошибка за плечами. Ещё можно всё исправить.",
                        "Рраф смотрит на меня. Я не могу его подвести.",
                        "Пятый день. Последний шанс."
                    };
                else
                    return new string[]
                    {
                        "Город уже не тот. Соседи не те. Я не та.",
                        "Дождь стучит по крыше. Рраф прижался к ногам.",
                        "Может, если я соберу всё правильно, звёзды отзовутся? Или уже поздно?"
                    };

            default:
                return new string[] { "" };
        }
    }
}