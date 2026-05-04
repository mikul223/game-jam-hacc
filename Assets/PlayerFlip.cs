using UnityEngine;

public class PlayerFlip : MonoBehaviour
{
    private bool facingRight = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A) && facingRight)
        {
            Flip();
        }

        if (Input.GetKeyDown(KeyCode.D) && !facingRight)
        {
            Flip();
        }
    }

    void LateUpdate()
    {
        Vector3 scale = transform.localScale;
        scale.y = Mathf.Abs(scale.y);
        scale.z = 1;
        transform.localScale = scale;
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}