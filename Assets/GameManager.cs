using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int currentDay = 1;       // текущий день (1, 2, 3)
    public int mistakes = 0;         // сколько ошибок сделано
    public int maxMistakes = 1;      // сколько ошибок можно без плохой концовки

    void Awake()
    {
        // Чтобы GameManager был один на всю игру
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

    public void AddMistake()
    {
        mistakes++;
        Debug.Log("Ошибка! Всего ошибок: " + mistakes);
    }

    public void NextDay()
    {
        currentDay++;
        Debug.Log("День " + currentDay + " начался");

        // Если ошибок <= 1 после сна — сбрасываем (NPC прощают)
        if (mistakes <= maxMistakes)
        {
            mistakes = 0;
            Debug.Log("NPC простили. Ошибки сброшены.");
        }
    }

    public string GetEnding()
    {
        if (mistakes == 0)
            return "Хорошая концовка! Все счастливы!";
        else if (mistakes <= maxMistakes)
            return "Средняя концовка. Было сложно, но всё наладилось.";
        else
            return "Плохая концовка. NPC изменились навсегда...";
    }
}