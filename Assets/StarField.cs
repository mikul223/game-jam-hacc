using UnityEngine;

public class StarField : MonoBehaviour
{
    public int starCount = 50;
    public float starSize = 0.5f;
    public Color starColor = Color.white;
    public float twinkleSpeedMin = 1f;
    public float twinkleSpeedMax = 4f;
    public float spreadX = 20f;
    public float spreadY = 15f;

    private GameObject[] stars;
    private float[] twinkleSpeeds;
    private Vector3[] startPositions;
    private Sprite starSprite;

    void Start()
    {
        starSprite = CreateStarSprite();

        stars = new GameObject[starCount];
        twinkleSpeeds = new float[starCount];
        startPositions = new Vector3[starCount];

        for (int i = 0; i < starCount; i++)
        {
            GameObject star = new GameObject("Star_" + i);
            star.transform.parent = transform;

            SpriteRenderer sr = star.AddComponent<SpriteRenderer>();
            sr.sprite = starSprite;
            sr.color = starColor;
            sr.sortingOrder = -5;

            float x = Random.Range(-spreadX / 2f, spreadX / 2f);
            float y = Random.Range(-spreadY / 2f, spreadY / 2f);
            star.transform.localPosition = new Vector3(x, y, 10); // Z=10 чтобы было перед камерой
            startPositions[i] = star.transform.localPosition;

            float size = Random.Range(0.5f, 1.5f) * starSize;
            star.transform.localScale = new Vector3(size, size, 1);

            twinkleSpeeds[i] = Random.Range(twinkleSpeedMin, twinkleSpeedMax);

            stars[i] = star;
        }

        Debug.Log($"Создано {starCount} звёзд");
    }

    void Update()
    {
        if (stars == null) return;

        for (int i = 0; i < stars.Length; i++)
        {
            if (stars[i] == null) continue;

            float alpha = (Mathf.Sin(Time.time * twinkleSpeeds[i]) + 1f) / 2f;
            Color c = stars[i].GetComponent<SpriteRenderer>().color;
            c.a = Mathf.Lerp(0.2f, 1f, alpha);
            stars[i].GetComponent<SpriteRenderer>().color = c;

            Vector3 move = startPositions[i] + new Vector3(
                Mathf.Sin(Time.time * 0.3f + i) * 0.15f,
                Mathf.Cos(Time.time * 0.3f + i) * 0.15f,
                0
            );
            stars[i].transform.localPosition = move;
        }
    }

    Sprite CreateStarSprite()
    {
        Texture2D tex = new Texture2D(8, 8);
        Color[] pixels = new Color[64];

        // Рисуем кружок
        for (int y = 0; y < 8; y++)
        {
            for (int x = 0; x < 8; x++)
            {
                float dist = Vector2.Distance(new Vector2(x, y), new Vector2(3.5f, 3.5f));
                if (dist < 3f)
                    pixels[y * 8 + x] = Color.white;
                else
                    pixels[y * 8 + x] = Color.clear;
            }
        }

        tex.SetPixels(pixels);
        tex.filterMode = FilterMode.Point;
        tex.Apply();

        return Sprite.Create(tex, new Rect(0, 0, 8, 8), new Vector2(0.5f, 0.5f), 32);
    }
}