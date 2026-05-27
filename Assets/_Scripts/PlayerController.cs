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
        movementComponent.SetDirection(value.Get<Vector2>());
    }
}
