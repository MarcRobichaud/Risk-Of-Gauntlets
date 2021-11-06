using UnityEngine;
using UnityEngine.Events;

public class InputSystem : MonoBehaviour
{
    public UnityEvent<Vector2> directionPressed;
    public UnityEvent OnDropBombPressed;

    public KeyCode DropBomb = KeyCode.Space;
    public KeyCode Up = KeyCode.UpArrow;
    public KeyCode Down = KeyCode.DownArrow;
    public KeyCode Right = KeyCode.RightArrow;
    public KeyCode Left = KeyCode.LeftArrow;

    private void Update()
    {
        Vector2 direction = new Vector2();

        if (Input.GetKey(Up))
            direction.y = 1;
        else if (Input.GetKey(Down))
            direction.y = -1;

        if (Input.GetKey(Right))
            direction.x = 1;
        else if (Input.GetKey(Left))
            direction.x = -1;

        directionPressed?.Invoke(direction);

        if (Input.GetKeyDown(DropBomb))
            OnDropBombPressed?.Invoke();
    }
}