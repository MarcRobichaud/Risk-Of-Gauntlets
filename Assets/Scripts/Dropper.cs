using System.Collections.Generic;
using UnityEngine;

public class Dropper : MonoBehaviour
{
    public List<Droppable> droppables;

    private int colliderLayer;

    private void Start()
    {
        colliderLayer = LayerMask.GetMask("Bomb");
    }

    public void Drop()
    {
        Collider2D[] collider2Ds = Physics2D.OverlapPointAll(transform.position.GetTilePosition(), colliderLayer);
        if (droppables.Count > 0 && collider2Ds.Length == 0)
        {
            droppables[0].Drop(transform.position.GetTilePosition(), this);
            droppables.RemoveAt(0);
        }
    }

    public void PickUp(Droppable droppable)
    {
        droppables.Add(droppable);
    }
}