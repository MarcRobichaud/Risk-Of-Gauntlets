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
    public static Vector2 GetPlayerPositionWithinBox(this Vector3 worldPosition)
    {
        Vector2 position = worldPosition;
        position.x = (position.x >= 0) ? (int)position.x + 0.5f : (int)position.x - 0.5f;
        position.y = (position.y >= 0) ? (int)(position.y + 0.5) : (int)(position.y - 0.5);
        return position;
    }

    public static Vector2 GetPositionWithinBox(this Vector3 worldPosition)
    {
        Vector2 position = worldPosition.GetPlayerPositionWithinBox();
        position.y += 0.5f;
        return position;
    }

    public static Vector2 GetOffsetPosition(this Vector3 position, Direction direction, int offset)
    {
        switch (direction)
        {
            case Direction.Up:
                position.y += offset;
                break;

            case Direction.Down:
                position.y -= offset;
                break;

            case Direction.Left:
                position.x += offset;
                break;

            case Direction.Right:
                position.x -= offset;
                break;

            default:
                break;
        }

        return position;
    }
}