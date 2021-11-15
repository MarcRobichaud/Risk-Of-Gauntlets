using System;
using UnityEngine;

public enum Direction
{
    Up,
    Down,
    Left,
    Right
}

public static class ExtensionFunc
{
    public static int RandNumber(int min, int max)
    {
        var rand = new System.Random();
        return rand.Next(min, max);
    }

    public static Direction GetRandomDirection()
    {
        var rand = new System.Random();
        Direction direction = (Direction)rand.Next((int)Direction.Right + 1);
        return direction;
    }

    public static Direction GetRandomDirection(Direction lastDirection)
    {
        var rand = new System.Random();
        Direction direction;
        do
        {
            direction = (Direction)rand.Next((int)Direction.Right + 1);
        }
        while (direction == lastDirection);
        return direction;
    }

    public static Vector2 GetPlayerTilePosition(this Vector3 worldPosition)
    {
        //transform.position.y of player is set to his feet by default
        Vector3 position = worldPosition;
        position.x = (position.x >= 0) ? (int)(position.x) + 0.5f : (int)(position.x) - 0.5f;
        position.y = (position.y >= 0) ? (int)(position.y + 0.5) : (int)(position.y - 0.5);
        return position;
    }

    public static Vector2 GetTilePosition(this Vector3 worldPosition)
    {
        Vector2 position = worldPosition;
        position.x = (position.x >= 0) ? (int)(position.x) + 0.5f : (int)(position.x) - 0.5f;
        position.y = (position.y >= 0) ? (int)(position.y) + 0.5f : (int)(position.y + 0.01) - 0.5f;
        return position;
    }

    public static Vector2 GetOffsetPosition(this Vector3 worldPosition, Direction direction, int offset) => direction switch
    {
        Direction.Up => worldPosition + new Vector3(0, offset),
        Direction.Down => worldPosition + new Vector3(0, -offset),
        Direction.Right => worldPosition + new Vector3(-offset, 0),
        Direction.Left => worldPosition + new Vector3(offset, 0),
        _ => throw new ArgumentOutOfRangeException(nameof(direction), $"Not expected direction value: {direction}"),
    };
}