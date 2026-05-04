using UnityEngine;

public class StarTwinkle : MonoBehaviour
{
    public float floatHeight = 0.2f;
    public float floatSpeed = 1.5f;
    public float twinkleSpeed = 3f;
    public float minAlpha = 0.2f;
    public float maxAlpha = 1f;
    public Color glowColor = Color.white;

    private SpriteRenderer spriteRenderer;
    private Vector3 startPos;
    private Material material;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        startPos = transform.localPosition;

        material = spriteRenderer.material;
    }

    void Update()
    {
        float floatOffset = Mathf.Sin(Time.time * floatSpeed) * floatHeight;
        transform.localPosition = startPos + new Vector3(0f, floatOffset, 0f);

        float alpha = Mathf.Lerp(minAlpha, maxAlpha, (Mathf.Sin(Time.time * twinkleSpeed) + 1f) / 2f);
        Color color = spriteRenderer.color;
        color.a = alpha;
        spriteRenderer.color = color;
    }
}