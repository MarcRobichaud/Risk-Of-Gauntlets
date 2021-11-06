using UnityEngine;
using UnityEngine.Tilemaps;

public class Destructable : MonoBehaviour
{
    public void Destruct(Collider2D collider, Vector2 position)
    {
        Tilemap tilemap = collider.gameObject.GetComponent<Tilemap>();
        tilemap.SetTile(tilemap.WorldToCell(position), null);
    }
}