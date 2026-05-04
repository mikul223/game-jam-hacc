using UnityEngine;

public class PlayerHead : MonoBehaviour
{
    public Sprite normalHead;
    public Sprite changedHead;

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (normalHead != null)
            spriteRenderer.sprite = normalHead;
    }

    void Update()
    {
        if (spriteRenderer == null) return;

        if (GameManager.instance != null && GameManager.instance.bodyChanged)
        {
            spriteRenderer.sprite = changedHead;
        }
        else
        {
            spriteRenderer.sprite = normalHead;
        }
    }
}