using UnityEngine;

public class HeadBob : MonoBehaviour
{
    public float bobSpeed = 10f;
    public float bobAmount = 0.05f;

    private Vector3 startPos;
    private float timer = 0f;

    void Start()
    {
        startPos = transform.localPosition;
    }

    void Update()
    {
        // Проверяем, движется ли игрок
        bool isMoving = Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D);

        if (isMoving)
        {
            timer += Time.deltaTime * bobSpeed;
            float offset = Mathf.Sin(timer) * bobAmount;
            transform.localPosition = startPos + new Vector3(0f, offset, 0f);
        }
        else
        {
            timer = 0f;
            transform.localPosition = Vector3.Lerp(transform.localPosition, startPos, Time.deltaTime * 10f);
        }
    }
}