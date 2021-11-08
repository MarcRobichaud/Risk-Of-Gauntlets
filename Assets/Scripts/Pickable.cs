using UnityEngine;
using UnityEngine.Events;

public class Pickable : MonoBehaviour
{
    public UnityEvent OnPickUp;
    public LayerMask pickableBy;

    private SpriteRenderer sprite;
    private Collider2D collider;

    private void Awake()
    {
        collider = GetComponent<Collider2D>();
        sprite = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (pickableBy == (pickableBy | (1 << collision.gameObject.layer)))
        {
            sprite.enabled = false;
            collider.enabled = false;
            OnPickUp?.Invoke();
            Destroy(gameObject);
        }
    }
}