using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int currentDay = 1;
    public int totalDays = 5;
    public int totalMistakes = 0;
    public int maxMistakesForGood = 1;
    public bool workDoneToday = false;
    public bool isRaining = false;

    public bool wasRandomMood = false;
    public bool savedWasRandom = false;

    public Mood savedMood = Mood.Normal;

    public int overRed = 0;
    public int overBlue = 0;
    public int overYellow = 0;
    public int overPurple = 0;

    public enum Mood { Normal, Angry, Sad, Happy, Scared }
    public Mood currentMood = Mood.Normal;

    void Start()
    {
        StartCoroutine(ShowIntroDelayed());
    }

    IEnumerator ShowIntroDelayed()
    {
        yield return new WaitForSeconds(0.1f);
        Canvas canvas = FindAnyObjectByType<Canvas>();
        DayIntro intro = canvas.GetComponent<DayIntro>();
        if (intro != null)
            intro.ShowDayIntro(currentDay);
    }

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

        FlowerManager fm = FlowerManager.instance;
        int maxOver = Mathf.Max(overRed, overBlue, overYellow, overPurple);

        bool allCorrect = (fm.redCount == fm.redNormal && 
                        fm.blueCount == fm.blueNormal && 
                        fm.yellowCount == fm.yellowNormal && 
                        fm.purpleCount == fm.purpleNormal);

        if (allCorrect)
        {
            currentMood = Mood.Normal;
            savedMood = Mood.Normal;
            wasRandomMood = false;
            savedWasRandom = false;
            Debug.Log("Всё собрано правильно.");
            return;
        }

        totalMistakes++;

        if (maxOver > 0)
        {
            if (overRed == maxOver) currentMood = Mood.Angry;
            else if (overBlue == maxOver) currentMood = Mood.Sad;
            else if (overYellow == maxOver) currentMood = Mood.Happy;
            else if (overPurple == maxOver) currentMood = Mood.Scared;
            wasRandomMood = false;
            Debug.Log($"Ошибка! Доминирующий цвет: {currentMood}. Всего ошибок: {totalMistakes}");
        }
        else
        {
            currentMood = Mood.Normal;
            wasRandomMood = true;
            Debug.Log($"Ошибка! Неполный сбор. NPC получат случайные эмоции. Всего ошибок: {totalMistakes}");
        }

        savedMood = currentMood;
        savedWasRandom = wasRandomMood;
        Debug.Log($"Эмоция сохранена: {savedMood}, random={savedWasRandom}");
    }

    public void NextDay()
    {
        currentDay++;
        Debug.Log("День " + currentDay + " начался");

        if (totalMistakes <= maxMistakesForGood)
        {
            currentMood = Mood.Normal;
            savedMood = Mood.Normal;
            wasRandomMood = false;
            savedWasRandom = false;
            isRaining = false;
            Debug.Log("Всё хорошо, NPC вернулись в норму.");
        }
        else
        {
            currentMood = savedMood;
            wasRandomMood = savedWasRandom;
            isRaining = true;
            Debug.Log($"Слишком много ошибок. NPC остались: {currentMood}, random={wasRandomMood}");
        }

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