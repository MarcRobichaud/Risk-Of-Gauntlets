using UnityEngine;

public class Enemies : MonoBehaviour
{
    private SpriteRenderer sprite;
    private Collider2D collid;
    private Moveable moveable;

    private Explodable explodable;
    private Direction lastDirection;

    private Vector2 movement;

    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        collid = GetComponent<Collider2D>();
        moveable = GetComponent<Moveable>();
        explodable = GetComponent<Explodable>();
        Reinitialize();
    }

    public void Reinitialize()
    {
        lastDirection = Direction.Up;
        sprite.enabled = true;
        collid.enabled = true;
        ChangeDirection();
    }

    private void ChangeDirection()
    {
        lastDirection = ExtensionFunc.GetRandomDirection(lastDirection);
        movement = new Vector3().GetOffsetPosition(lastDirection, 1);
        moveable.Move(movement);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        ChangeDirection();
        Hitable hitable = collision.gameObject.GetComponent<Hitable>();
        if (hitable)
            hitable.Hit(collision.collider, collision.transform.position, gameObject.layer);
    }

    public void OnHit()
    {
        Die();
        if (explodable)
            explodable.StartExploding();
    }

    private void Die()
    {
        sprite.enabled = false;
        collid.enabled = false;
        moveable.Move(new Vector2(0, 0));
    }
}