using UnityEngine;

public class Enemies : MonoBehaviour
{
    public Pickable bombUpgrade;
    public Pickable bomb;
    public float bombCooldown = 6;

    private bool isAlive = true;
    private float timeStarted;
    private SpriteRenderer sprite;
    private Collider2D collid;
    private Moveable moveable;
    private Explodable explodable;
    private Dropper dropper;
    private Direction lastDirection;

    private Vector2 movement;

    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        collid = GetComponent<Collider2D>();
        moveable = GetComponent<Moveable>();
        explodable = GetComponent<Explodable>();
        dropper = GetComponent<Dropper>();
        Reinitialize();
    }

    private void Update()
    {
        if (dropper && Time.time > timeStarted + bombCooldown && isAlive)
        {
            dropper.Drop();
            timeStarted = Time.time;
        }
    }

    public void Reinitialize()
    {
        timeStarted = Time.time;
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
            explodable.StartTicking(5);
    }

    private void Die()
    {
        isAlive = false;
        int i = ExtensionFunc.RandNumber(0, 2);
        Pickable newPickable = (i == 0) ? Instantiate(bombUpgrade) : Instantiate(bomb);
        newPickable.transform.position = transform.position.GetTilePosition();
        newPickable.gameObject.SetActive(true);
        sprite.enabled = false;
        collid.enabled = false;
        moveable.Move(new Vector2(0, 0));
    }
}