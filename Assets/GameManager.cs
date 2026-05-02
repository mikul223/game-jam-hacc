using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int currentDay = 1;
    public int totalDays = 5;
    public int totalMistakes = 0;       // сколько дней с ошибками
    public int maxMistakesForGood = 1;  // если ≤1 — хорошая концовка
    public bool workDoneToday = false;
    public bool isRaining = false;

    // Переборы цветов
    public int overRed = 0;
    public int overBlue = 0;
    public int overYellow = 0;
    public int overPurple = 0;

    public enum Mood { Normal, Angry, Sad, Happy, Scared }
    public Mood currentMood = Mood.Normal;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        isRaining = true;
    }

    public void CalculateOvercollect()
    {
        FlowerManager fm = FlowerManager.instance;
        if (fm == null) return;

        overRed = Mathf.Max(0, fm.redCount - fm.redNormal);
        overBlue = Mathf.Max(0, fm.blueCount - fm.blueNormal);
        overYellow = Mathf.Max(0, fm.yellowCount - fm.yellowNormal);
        overPurple = Mathf.Max(0, fm.purpleCount - fm.purpleNormal);
    }

    public void DetermineMood()
    {
        CalculateOvercollect();

        int maxOver = Mathf.Max(overRed, overBlue, overYellow, overPurple);

        if (maxOver == 0)
        {
            currentMood = Mood.Normal;
            return;
        }

        // Какой цвет перебрали больше всего
        if (overRed == maxOver) currentMood = Mood.Angry;
        else if (overBlue == maxOver) currentMood = Mood.Sad;
        else if (overYellow == maxOver) currentMood = Mood.Happy;
        else if (overPurple == maxOver) currentMood = Mood.Scared;

        // Если есть хоть какой-то перебор — это ошибка
        totalMistakes++;
        Debug.Log($"Ошибка! Доминирующий цвет: {currentMood}. Всего ошибок: {totalMistakes}");
    }

    public void NextDay()
    {
        currentDay++;
        Debug.Log("День " + currentDay + " начался");

        if (totalMistakes <= maxMistakesForGood)
        {
            // Ошибок мало — всё сбрасывается
            currentMood = Mood.Normal;
            isRaining = false;
            Debug.Log("Всё хорошо, NPC вернулись в норму.");
        }
        else
        {
            // Ошибок много — дождь и NPC не сбрасываются
            isRaining = true;
            Debug.Log("Слишком много ошибок. Дождь идёт, NPC не вернулись.");
        }

        // Сброс дневных счётчиков
        overRed = 0;
        overBlue = 0;
        overYellow = 0;
        overPurple = 0;
    }

    public string GetEnding()
    {
        if (totalMistakes <= maxMistakesForGood)
            return "Хорошая концовка! Вы молодец, всё делали правильно.";
        else
            return "Плохая концовка. Вас возненавидели, не надо было экспериментировать.";
    }
}