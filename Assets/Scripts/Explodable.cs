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
    public float lengthOfTicking = 3;
    public float lengthOfExplosion = 0.25f;
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

    public void StartTicking(int explosionRange)
    {
        explosion.explosionRange = explosionRange;
        if (explodableState == ExplodableState.Waiting)
        {
            if (droppable)
                OnExplode.AddListener(droppable.dropper.PickUp);
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
        {
            OnExplode?.Invoke(droppable);
            OnExplode.RemoveListener(droppable.dropper.PickUp);
        }
        explodableState = ExplodableState.Waiting;
    }
}