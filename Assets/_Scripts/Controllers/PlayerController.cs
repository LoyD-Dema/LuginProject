using System;
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
    private ShootComponent playerShootComponent;
    public event Action OnPausePressed;
    public static event Action OnReloadEvent;

    [SerializeField] private ChooseChamber choseChamberUI;

    private bool isPaused;
    public bool IsPaused
    {
        get => isPaused;
        set => isPaused = value;
    }

    private void Awake()
    {
        movementComponent = GetComponent<MovementComponent>();
        playerShootComponent = GetComponent<ShootComponent>();
    }

    public void OnShoot(InputValue value)
    {
        if (isPaused) return;

        playerShootComponent.Shoot();
    }

    public void OnMove(InputValue value)
    {
        if (isPaused) return;

        Vector2 input = value.Get<Vector2>();

        if (input == Vector2.zero)
        {
            movementComponent.SetDirection(input);
            return;
        }
        Vector3 cameraForward = Vector3.ProjectOnPlane(Camera.main.transform.forward, Vector3.up).normalized;
        Vector3 cameraRight = Vector3.ProjectOnPlane(Camera.main.transform.right, Vector3.up).normalized;

        Vector3 direction = (cameraForward * input.y) + (cameraRight * input.x);
        movementComponent.SetDirection(new Vector2(direction.x, direction.z));
    }

    public void OnPause(InputValue value)
    {
        if (value.isPressed)
        {
            OnPausePressed?.Invoke();
        }
    }
    //TODO: Mouse movement can be added here, as well as shooting and other actions that the player can do.

    public void OnReload(InputValue value)
    {
        OnReloadEvent?.Invoke();
        AudioManager.PlaySound3D(SoundType.Reload, transform.position);
    }

    #region UI Input
    public void OnNavigate(InputValue value)
    {
        if (!choseChamberUI.gameObject.activeInHierarchy) return;

        Vector2 navValue = value.Get<Vector2>();
        if (navValue.x < -0.5f)
        {
            choseChamberUI.MoveLeft();
        }
        else if (navValue.x > 0.5f)
        {
            choseChamberUI.MoveRight();
        }
    }

    public void OnSubmit(InputValue value)
    {
        if (choseChamberUI.gameObject.activeInHierarchy)
        {
            choseChamberUI.SelectChamber();
        }
    }



    #endregion
}
