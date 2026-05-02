using UnityEngine;

public class FlowerManager : MonoBehaviour
{
    public static FlowerManager instance;

    public int redCount = 0;
    public int blueCount = 0;
    public int yellowCount = 0;
    public int purpleCount = 0;

    public int redNormal = 1;
    public int blueNormal = 1;
    public int yellowNormal = 1;
    public int purpleNormal = 1;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    public void CollectFlower(string color)
    {
        switch (color)
        {
            case "Red": redCount++; break;
            case "Blue": blueCount++; break;
            case "Yellow": yellowCount++; break;
            case "Purple": purpleCount++; break;
        }
        Debug.Log($"Красные: {redCount}/{redNormal}, Синие: {blueCount}/{blueNormal}, Жёлтые: {yellowCount}/{yellowNormal}, Фиолетовые: {purpleCount}/{purpleNormal}");
    }

    public bool IsAllCorrect()
    {
        return redCount == redNormal &&
               blueCount == blueNormal &&
               yellowCount == yellowNormal &&
               purpleCount == purpleNormal;
    }

    public int GetOvercollectCount()
    {
        int over = 0;
        if (redCount > redNormal) over++;
        if (blueCount > blueNormal) over++;
        if (yellowCount > yellowNormal) over++;
        if (purpleCount > purpleNormal) over++;
        return over;
    }

    public void ResetCounts()
    {
        redCount = 0;
        blueCount = 0;
        yellowCount = 0;
        purpleCount = 0;
    }
}