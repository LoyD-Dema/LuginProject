using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// This class is responsible for handling the player's input.
/// It uses the new Input System package to handle input, and it requires a PlayerInput component to be attached to
/// the same GameObject. 
/// </summary>

[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(MovementComponent))]

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
    
    //TODO: Mouse movement can be added here, as well as shooting and other actions that the player can do.
}
