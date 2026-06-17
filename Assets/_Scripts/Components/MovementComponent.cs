using UnityEngine;


[RequireComponent (typeof(Rigidbody))]
public class MovementComponent : MonoBehaviour
{

    [SerializeField] private float speed = 10.0f;
    [SerializeField] private float speedMultiplayer = 1;
    public float SpeedMultiplayer { get { return speedMultiplayer; } set { speedMultiplayer = value; }}

    private Rigidbody rb;
    private Vector3 direction;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void SetDirection(Vector2 direction)
    {
        this.direction = new Vector3(direction.x, 0f, direction.y);
    }
    
    private void FixedUpdate()
    {
        float actualSpeed = speed * SpeedMultiplayer;
        rb.linearVelocity = new Vector3(direction.x * actualSpeed, rb.linearVelocity.y , direction.z * actualSpeed);
    }
}
