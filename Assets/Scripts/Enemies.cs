using UnityEngine;

public class Enemies : MonoBehaviour
{
    public LayerMask layerMask;

    private Direction lastDirection;
    private Collider2D collid;
    private Moveable moveable;
    private Vector2 movement;

    private void Start()
    {
        moveable = GetComponent<Moveable>();
        collid = GetComponent<Collider2D>();
        Reinitialize();
    }

    public void Reinitialize()
    {
        lastDirection = Direction.Up;
        collid.enabled = true;
        ChangeDirection();
    }

    private void FixedUpdate()
    {
        moveable.Move(movement);
    }

    private void ChangeDirection()
    {
        lastDirection = ExtensionFunc.GetRandomDirection(lastDirection);
        Debug.Log(lastDirection);
        movement = new Vector3().GetOffsetPosition(lastDirection, 1);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        ChangeDirection();
    }
}