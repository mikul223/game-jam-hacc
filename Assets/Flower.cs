using UnityEngine;

public class Flower : MonoBehaviour
{
    public string flowerColor = "Red"; // Red, Blue, Yellow, Purple
    public GameObject collectPrompt;

    private bool playerNear = false;

    void Update()
    {
        if (playerNear && Input.GetKeyDown(KeyCode.F))
        {
            Collect();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && collectPrompt != null)
        {
            playerNear = true;
            collectPrompt.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && collectPrompt != null)
        {
            playerNear = false;
            collectPrompt.SetActive(false);
        }
    }

    void Collect()
    {
        FlowerManager.instance.CollectFlower(flowerColor);
    }
}