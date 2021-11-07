using UnityEngine;
using UnityEngine.Events;

public class Hitable : MonoBehaviour
{
    public LayerMask layersHitableBy; //Layers that will invoke the OnHit Event
    public UnityEvent<Collider2D, Vector2> OnHit;

    public void Hit(Collider2D collider, Vector2 position, int layer)
    {
        if (layersHitableBy == (layersHitableBy | (1 << layer)))
            OnHit?.Invoke(collider, position);
    }
}