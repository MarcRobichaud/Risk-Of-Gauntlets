using UnityEngine;
using UnityEngine.Events;

public class Hitable : MonoBehaviour
{
    public UnityEvent<Collider2D, Vector2> OnHit;

    public void Hit(Collider2D collider, Vector2 position)
    {
        OnHit?.Invoke(collider, position);
    }
}