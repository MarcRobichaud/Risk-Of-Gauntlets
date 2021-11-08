using UnityEngine;
using UnityEngine.Events;

public class Droppable : MonoBehaviour
{
    public UnityEvent<int> OnDroppableDropped;
    [HideInInspector] public Dropper dropper;

    private SpriteRenderer spriteRenderer;
    private Collider2D collid;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        collid = GetComponent<Collider2D>();
    }

    public void Drop(Vector2 position, Dropper _dropper)
    {
        dropper = _dropper;
        transform.position = position;
        collid.enabled = true;
        collid.isTrigger = true;
        spriteRenderer.enabled = true;
        OnDroppableDropped?.Invoke(dropper.dropLevel);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetHashCode() == dropper.gameObject.GetHashCode())
            collid.isTrigger = false;
    }
}