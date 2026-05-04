using UnityEngine;

public class DogFollow : MonoBehaviour
{
    public Transform target;
    public float followDelay = 0.3f;
    public Vector3 offset = new Vector3(-1f, -0.5f, 0f);

    private Vector3 velocity = Vector3.zero;
    private bool facingRight = true;

    void Update()
    {
        if (target == null) return;

        float targetScaleX = target.localScale.x;
        bool targetFacingRight = targetScaleX > 0;

        Vector3 offsetDirection = offset;
        if (!targetFacingRight)
            offsetDirection.x = -offset.x;

        Vector3 targetPos = target.position + offsetDirection;

        transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, followDelay);

        if (targetFacingRight != facingRight)
        {
            facingRight = targetFacingRight;
            Vector3 scale = transform.localScale;
            scale.x = Mathf.Abs(scale.x) * (facingRight ? 1 : -1);
            transform.localScale = scale;
        }
    }
}