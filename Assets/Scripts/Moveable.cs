using UnityEngine;

public class Moveable : MonoBehaviour
{
    public float speed = 5f;

    private Animator anim;
    private Rigidbody2D rb;
    private Vector2 lastDirection = new Vector2();

    private void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    public void Move(Vector2 direction)
    {
        if (direction != lastDirection)
            transform.position = transform.position.GetPlayerPositionWithinBox();

        anim.SetInteger("HorzSpeed", (int)direction.x);
        anim.SetInteger("VertSpeed", (int)direction.y);

        rb.velocity = direction * speed;
        lastDirection = direction;
    }
}