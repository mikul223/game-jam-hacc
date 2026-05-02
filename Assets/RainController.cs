using UnityEngine;

public class RainController : MonoBehaviour
{
    void Update()
    {
        GameManager gm = GameManager.instance;
        if (gm == null) return;

        if (gm.isRaining && !gameObject.activeSelf)
            gameObject.SetActive(true);
        else if (!gm.isRaining && gameObject.activeSelf)
            gameObject.SetActive(false);
    }
}