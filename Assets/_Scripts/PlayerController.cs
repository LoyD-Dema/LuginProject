using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private MovementComponent movementComponent;
    private ShootComponent shootComponent;

    private void Awake()
    {
        movementComponent = GetComponent<MovementComponent>();
        shootComponent = GetComponent<ShootComponent>();
    }

    public void OnShoot(InputValue value)
    {
        shootComponent.Shoot();
    }

    public void OnMove(InputValue value)
    {
        movementComponent.SetDirection(value.Get<Vector2>());
    }
    
}
