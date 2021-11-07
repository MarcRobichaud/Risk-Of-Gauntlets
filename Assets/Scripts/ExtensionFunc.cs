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

    public static Vector2 GetOffsetPosition(this Vector3 worldPosition, Direction direction, int offset)
    {
        switch (direction)
        {
            case Direction.Up:
                worldPosition.y += offset;
                break;

            case Direction.Down:
                worldPosition.y -= offset;
                break;

            case Direction.Left:
                worldPosition.x -= offset;
                break;

            case Direction.Right:
                worldPosition.x += offset;
                break;

            default:
                break;
        }

        return worldPosition;
    }
}