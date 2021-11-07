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
    public int explosionRange = 1;
    public float lengthOfTicking = 3;
    public float lengthOfExplosion = 1;
    public Explosion explosionPrefab;
    public UnityEvent<Droppable> OnExplode;

    private Explosion explosion;
    private Collider2D collid;
    private Droppable droppable;
    private SpriteRenderer spriteRenderer;
    private ExplodableState explodableState;
    private float timeStarted;

    private void Awake()
    {
        collid = GetComponent<Collider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        droppable = GetComponent<Droppable>();
        explosion = Instantiate(explosionPrefab);
        explosion.explosionRange = explosionRange;
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
        explosion.StartExplosion(transform.position.GetTilePosition());
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