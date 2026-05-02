using UnityEngine;
using TMPro;

public class FlowerUI : MonoBehaviour
{
    public TextMeshProUGUI counterText;

    void Update()
    {
        if (FlowerManager.instance == null) return;

        counterText.text = $"red{FlowerManager.instance.redCount}/{FlowerManager.instance.redNormal}  " +
                           $"blue{FlowerManager.instance.blueCount}/{FlowerManager.instance.blueNormal}  " +
                           $"yellow{FlowerManager.instance.yellowCount}/{FlowerManager.instance.yellowNormal}  " +
                           $"purple{FlowerManager.instance.purpleCount}/{FlowerManager.instance.purpleNormal}";
    }
}