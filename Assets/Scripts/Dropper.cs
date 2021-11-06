using System.Collections.Generic;
using UnityEngine;

public class Dropper : MonoBehaviour
{
    public List<Droppable> droppables;

    public void Drop()
    {
        Collider2D[] collider2Ds = Physics2D.OverlapPointAll(transform.position.GetPositionWithinBox(), LayerMask.GetMask("Bomb"));
        if (droppables.Count > 0 && collider2Ds.Length == 0)
        {
            droppables[0].Drop(transform.position.GetPositionWithinBox(), this);
            droppables.RemoveAt(0);
        }
    }

    public void PickUp(Droppable droppable)
    {
        droppables.Add(droppable);
    }
}