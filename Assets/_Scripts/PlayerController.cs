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
        GetComponent<Renderer>().material.color = Color.green;
    }

    private void OnShoot(InputValue value)
    {
        shootComponent.Shoot();
    }

    private void OnMove(InputValue value)
    {
        movementComponent.SetDirection(value.Get<Vector2>());
    }
    

    private void OnReload(InputValue value)
    {
        shootComponent.Reload();
    }
}
