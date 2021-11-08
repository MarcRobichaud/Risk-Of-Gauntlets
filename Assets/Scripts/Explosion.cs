using System;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public SpriteRenderer centerSprite;
    public List<SpriteRenderer> directionSprites;
    public int explosionRange = 2;

    private LayerMask stopLayer;
    private int colliderLayer;
    private Vector2 TileSize = new Vector2(0.95f, 0.95f);

    private int Offset(int i) => i + 1;

    private int ZRotation(Direction direction) => direction switch
    {
        Direction.Up => -90,
        Direction.Down => 90,
        Direction.Right => 180,
        Direction.Left => 0,
        _ => throw new ArgumentOutOfRangeException(nameof(direction), $"Not expected direction value: {direction}"),
    };

    private void Start()
    {
        stopLayer = LayerMask.GetMask("Wall", "DestructibleWall");
        colliderLayer = LayerMask.GetMask("Bomb", "Player", "Wall", "DestructibleWall", "Enemies", "Wanderer");
        Reinitialize();
    }

    public void Reinitialize()
    {
        centerSprite.enabled = false;
        foreach (var directionSprite in directionSprites)
            directionSprite.enabled = false;
    }

    public void StartExplosion(Vector2 explosionPosition)
    {
        CheckCollisions(explosionPosition);
    }

    private void CheckCollisions(Vector2 position)
    {
        CheckCollisionAt(position); //center
        DrawCenterExplosion(position);

        foreach (Direction direction in (Direction[])System.Enum.GetValues(typeof(Direction))) //foreach directions
            CheckCollisionAtDirection(direction);
    }

    private void CheckCollisionAtDirection(Direction direction)
    {
        for (int i = 0; i < explosionRange; i++)
        {
            Vector2 position = transform.position.GetOffsetPosition(direction, Offset(i));
            DrawDirectionExplosion(position, direction);
            if (CheckCollisionAt(position))
                break;
        }
    }

    //Call the hitable objects on hit and return whether the explosion should be stop or not
    private bool CheckCollisionAt(Vector2 position)
    {
        Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(position, TileSize, 0, colliderLayer);
        bool stopped = false;

        foreach (Collider2D collider in collider2Ds)
        {
            Hitable hitable = collider.gameObject.GetComponent<Hitable>();

            if (hitable)
                hitable.Hit(collider, position, gameObject.layer);

            if (stopLayer == (stopLayer | (1 << collider.gameObject.layer)))
                stopped = true;
        }
        return stopped;
    }

    public void DrawCenterExplosion(Vector2 position)
    {
        transform.position = position;
        centerSprite.enabled = true;
    }

    public void DrawDirectionExplosion(Vector2 position, Direction direction)
    {
        bool isDrawn = false;
        foreach (var directionSprite in directionSprites)
        {
            if (!directionSprite.enabled)
            {
                isDrawn = true;
                directionSprite.enabled = true;
                UpdateSprite(directionSprite, position, ZRotation(direction));
                break;
            }
        }
        if (!isDrawn)
        {
            directionSprites.Add(InstantiateDirectionSprite());
            UpdateSprite(directionSprites[directionSprites.Count - 1], position, ZRotation(direction));
        }
    }

    private SpriteRenderer InstantiateDirectionSprite()
    {
        return Instantiate(directionSprites[0], transform, true);
    }

    private void UpdateSprite(SpriteRenderer sprite, Vector2 position, int ZRotation)
    {
        sprite.transform.position = position;
        sprite.transform.eulerAngles = new Vector3(0, 0, ZRotation);
        sprite.enabled = true;
    }
}