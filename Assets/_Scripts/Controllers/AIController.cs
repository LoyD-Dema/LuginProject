using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    protected enum States
    {
        None,
        Moving,
        Shooting,
        LAST
    }

    [SerializeField]
    private Transform target;
    [SerializeField]
    private float shootRadius = 5;
    [SerializeField]
    private float followRadius = 10; // When the enemy starts shooting it will go back to the follow state when this distance is crossed

    // DEBUG
    private Dictionary<States, Color> statesColors = new Dictionary<States, Color>((int)States.LAST);

    private States currentState = States.Moving;

    private MovementComponent movementComponent;
    private ShootComponent _playerShootComponent;
    private Material material;

    private void Start()
    {
        movementComponent = GetComponent<MovementComponent>();
        _playerShootComponent = GetComponent<ShootComponent>();
        material = GetComponentInChildren<Renderer>().material;

        statesColors[States.Moving] = Color.blue;
        statesColors[States.Shooting] = Color.red;
    }

    private void OnEnable()
    {
        currentState = States.Moving;
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void OnDisable()
    {
        currentState = States.None;
    }

    private void Update()
    {
        if (!target) return;

        Vector3 dst = target.position - transform.position;
        transform.forward = dst.normalized;

        switch (currentState)
        {
            case States.Moving:
                if (dst.sqrMagnitude <= shootRadius * shootRadius)
                {
                    currentState = States.Shooting;
                    movementComponent.SetDirection(Vector2.zero);
                    break;
                }

                movementComponent.SetDirection(new Vector2(transform.forward.x, transform.forward.z));

                break;
            case States.Shooting:
                
                _playerShootComponent.Shoot();
                
                if (dst.sqrMagnitude > followRadius * followRadius)
                {
                    currentState = States.Moving;
                }

                break;
            default:
                Debug.Log("Nessuno stato trovato :c");
                break; 
        }

        material.color = statesColors[currentState];
    }
}
