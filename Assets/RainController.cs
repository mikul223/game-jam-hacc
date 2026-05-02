using UnityEngine;

public class RainController : MonoBehaviour
{
    void Start()
    {
        GameManager gm = GameManager.instance;
        if (gm != null && gm.isRaining)
        {
            gameObject.SetActive(true);
            Debug.Log("RAIN: Дождь включён при старте!");
        }
        else
        {
            Debug.Log("RAIN: gm=" + (gm != null) + " isRaining=" + (gm != null ? gm.isRaining : false));
        }
    }

    void Update()
    {
        GameManager gm = GameManager.instance;
        if (gm == null) return;
        gameObject.SetActive(gm.isRaining);
    }
}