using UnityEngine;
using UnityEngine.Events;

internal enum ExplodableState
{
    Waiting,
    Ticking,
    Explode
}

public class Explodable : MonoBehaviour
{
    public int explosionRange = 2;
    public float lengthOfTicking = 3;
    public float lengthOfExplosion = 1;
    public Explosion explosionPrefab;
    public UnityEvent<Droppable> OnExplode;

    private Explosion explosion;
    private Vector2 TileSize = new Vector2(0.95f, 0.95f);
    private Collider2D collid;
    private Droppable droppable;
    private SpriteRenderer spriteRenderer;
    private ExplodableState explodableState;
    private float timeStarted;

    private int Offset(int i) => i + 1;

    private void Awake()
    {
        collid = GetComponent<Collider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        droppable = GetComponent<Droppable>();
        explosion = Instantiate(explosionPrefab);
    }

    private void Update()
    {
        switch (explodableState)
        {
            case ExplodableState.Ticking:
                Ticking();
                break;

            case ExplodableState.Explode:
                Explode();
                break;

            default:
                break;
        }
    }

    private void Ticking()
    {
        if (Time.time > timeStarted + lengthOfTicking)
            StartExploding();
    }

    private void Explode()
    {
        if (Time.time > timeStarted + lengthOfExplosion)
            StartWaiting();
    }

    private void CheckCollisions()
    {
        CheckCollisionAt(transform.position); //center
        explosion.DrawCenterExplosion(transform.position);

        foreach (Direction direction in (Direction[])System.Enum.GetValues(typeof(Direction))) //foreach directions
            CheckCollisionAtDirection(direction);
    }

    private void CheckCollisionAtDirection(Direction direction)
    {
        for (int i = 0; i < explosionRange; i++)
        {
            Vector2 position = transform.position.GetOffsetPosition(direction, Offset(i));
            if (!CheckCollisionAt(position))
                explosion.DrawDirectionExplosion(position, direction);
            else
                break;
        }
    }

    //return whether it hit a wall or not
    private bool CheckCollisionAt(Vector2 position)
    {
        Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(position, TileSize, 0, LayerMask.GetMask("Bomb", "Player", "Wall", "Enemies"));
        bool hitAWall = false;

        foreach (Collider2D collider in collider2Ds)
        {
            Hitable hitable = collider.gameObject.GetComponent<Hitable>();

            if (hitable)
                hitable.Hit(collider, position);

            if (collider.gameObject.layer == LayerMask.NameToLayer("Wall"))
                hitAWall = true;
        }
        return hitAWall;
    }

    public void StartTicking()
    {
        if (explodableState == ExplodableState.Waiting)
        {
            timeStarted = Time.time;
            explodableState = ExplodableState.Ticking;
        }
    }

    public void StartExploding()
    {
        collid.enabled = false;
        spriteRenderer.enabled = false;
        CheckCollisions();
        timeStarted = Time.time;
        explodableState = ExplodableState.Explode;
    }

    private void StartWaiting()
    {
        explosion.Reinitialize();
        if (droppable)
            OnExplode?.Invoke(droppable);
        explodableState = ExplodableState.Waiting;
    }
}