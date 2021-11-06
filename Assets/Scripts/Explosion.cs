using System;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public SpriteRenderer centerSprite;
    public List<SpriteRenderer> directionSprites;

    private void Start()
    {
        Reinitialize();
    }

    public void Reinitialize()
    {
        centerSprite.enabled = false;
        foreach (var directionSprite in directionSprites)
            directionSprite.enabled = false;
    }

    private int ZRotation(Direction direction) => direction switch
    {
        Direction.Up => -90,
        Direction.Down => 90,
        Direction.Right => 180,
        Direction.Left => 0,
        _ => throw new ArgumentOutOfRangeException(nameof(direction), $"Not expected direction value: {direction}"),
    };

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