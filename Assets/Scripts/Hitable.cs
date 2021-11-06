using UnityEngine;
using UnityEngine.Events;

public class Hitable : MonoBehaviour
{
    public UnityEvent OnHit;
    public UnityEvent<Collider2D, Vector2> OnHitColl;

    public void Hit(Collider2D collider, Vector2 position)
    {
        OnHit?.Invoke();
        OnHitColl?.Invoke(collider, position);
    }
}