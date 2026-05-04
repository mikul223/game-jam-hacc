using UnityEngine;
using TMPro;

public class FlowerUIImages : MonoBehaviour
{
    public TextMeshProUGUI redText;
    public TextMeshProUGUI blueText;
    public TextMeshProUGUI yellowText;
    public TextMeshProUGUI purpleText;

    void Update()
    {
        if (FlowerManager.instance == null) return;

        redText.text = $"{FlowerManager.instance.redCount}/{FlowerManager.instance.redNormal}";
        blueText.text = $"{FlowerManager.instance.blueCount}/{FlowerManager.instance.blueNormal}";
        yellowText.text = $"{FlowerManager.instance.yellowCount}/{FlowerManager.instance.yellowNormal}";
        purpleText.text = $"{FlowerManager.instance.purpleCount}/{FlowerManager.instance.purpleNormal}";
    }
}