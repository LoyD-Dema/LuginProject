using UnityEngine;

public class AIController : MonoBehaviour
{
    [SerializeField]
    private Transform target;
    [SerializeField]
    private float shootRadiusSquared;

    private MovementComponent movementComponent;

    private void Start()
    {
        movementComponent = GetComponent<MovementComponent>();
    }

    private void Update()
    {
        Vector3 dst = target.position - transform.position;
        transform.forward = dst.normalized;

        if(dst.sqrMagnitude >= shootRadiusSquared * shootRadiusSquared)
        {
            movementComponent.SetDirection(new Vector2(transform.forward.x, transform.forward.z));
            return;
        }

        movementComponent.SetDirection(Vector2.zero);
    }
}
