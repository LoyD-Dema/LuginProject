using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private MovementComponent movementComponent;
    private Vector2 direction;

    private void Awake()
    {
        movementComponent = GetComponent<MovementComponent>();
    }

    public void OnMove(InputValue value)
    {
        direction = value.Get<Vector2>();

        movementComponent.SetDirection(direction);
    }
}
