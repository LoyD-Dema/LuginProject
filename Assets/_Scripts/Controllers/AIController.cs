using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class AIController : MonoBehaviour
{
    protected enum States
    {
        None,
        Moving,
        Shooting,
        LAST
    }

    [Header("Stats")]
    [SerializeField]
    private Transform target;
    [SerializeField]
    private float shootRadius = 5;
    [SerializeField]
    private float followRadius = 10; // When the enemy starts shooting it will go back to the follow state when this distance is crossed

    // DEBUG
    private Dictionary<States, Color> statesColors = new Dictionary<States, Color>((int)States.LAST);

    private States currentState = States.Moving;

    //private MovementComponent movementComponent; // Not needed anymore since then navmesh agent handles everything
    private ShootComponent playerShootComponent;
    private NavMeshAgent navAgent;
    private Material material;

    private void Start()
    {
        //movementComponent = GetComponent<MovementComponent>();
        playerShootComponent = GetComponent<ShootComponent>();
        navAgent = GetComponent<NavMeshAgent>();
        material = GetComponentInChildren<Renderer>().material;

        statesColors[States.Moving] = Color.blue;
        statesColors[States.Shooting] = Color.red;
    }

    private void OnEnable()
    {
        currentState = States.Moving;
        target = GameObject.FindGameObjectWithTag("Player").transform;
        if(!navAgent) navAgent = GetComponent<NavMeshAgent>();
    }

    private void OnDisable()
    {
        currentState = States.None;
    }

    private void Update()
    {
        if (!target) return;

        Vector3 dst = target.position - transform.position;
        Vector3 dstNormalized = dst.normalized;
        transform.forward = new Vector3(dstNormalized.x, transform.forward.y, dstNormalized.z);

        switch (currentState)
        {
            case States.Moving:
                if (dst.sqrMagnitude <= shootRadius * shootRadius)
                {
                    currentState = States.Shooting;
                    navAgent.isStopped = true;
                    //movementComponent.SetDirection(Vector2.zero);
                    break;
                }

                navAgent.SetDestination(target.position);
                //movementComponent.SetDirection(new Vector2(transform.forward.x, transform.forward.z));

                break;
            case States.Shooting:
                
                playerShootComponent.Shoot();
                
                if (dst.sqrMagnitude > followRadius * followRadius)
                {
                    currentState = States.Moving;
                    navAgent.isStopped = false;
                }

                break;
            default:
                Debug.Log("Nessuno stato trovato :c");
                break; 
        }

        material.color = statesColors[currentState];
    }
}
