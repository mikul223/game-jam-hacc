using UnityEngine;

public class StarTwinkle : MonoBehaviour
{
    public float floatHeight = 0.2f;
    public float floatSpeed = 1.5f;
    public float twinkleSpeed = 3f;     // скорость мерцания
    public float minAlpha = 0.2f;       // минимальная прозрачность
    public float maxAlpha = 1f;         // максимальная прозрачность
    public Color glowColor = Color.white; // цвет свечения

    private SpriteRenderer spriteRenderer;
    private Vector3 startPos;
    private Material material;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        startPos = transform.localPosition;

        // Создаём копию материала, чтобы менять только у этого объекта
        material = spriteRenderer.material;
    }

    void Update()
    {
        // Парение вверх-вниз
        float floatOffset = Mathf.Sin(Time.time * floatSpeed) * floatHeight;
        transform.localPosition = startPos + new Vector3(0f, floatOffset, 0f);

        // Мерцание (меняем альфу)
        float alpha = Mathf.Lerp(minAlpha, maxAlpha, (Mathf.Sin(Time.time * twinkleSpeed) + 1f) / 2f);
        Color color = spriteRenderer.color;
        color.a = alpha;
        spriteRenderer.color = color;
    }
}