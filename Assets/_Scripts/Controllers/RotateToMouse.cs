using UnityEngine;
using Utilities;

//TODO: move this in the PlayerController?
public class RotateToMouse : MonoBehaviour
{
    [Range(1.0f, 50.0f)]
    [SerializeField] float rotationSpeed = 20.0f;
    [SerializeField] private Transform shootPosition;

    // Debug
    [Header("Debug")]
    [SerializeField] private bool enableDebug;
    [SerializeField] private Vector3 size;
    private Vector3 center;


    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(MouseInput.GetMousePositon());
        Plane p = new Plane(Vector3.up, new Vector3(transform.position.x, transform.position.y + shootPosition.localPosition.y, transform.position.z));
        
        Debug.DrawRay(ray.origin, ray.direction * 100f, Color.green);

        Vector3 newPointOnPlane = Vector3.zero;

        if (p.Raycast(ray, out float enter))
        {
            newPointOnPlane = ray.GetPoint(enter);
            center = newPointOnPlane;
        }

        Vector3 dir = (newPointOnPlane - new Vector3(transform.position.x, newPointOnPlane.y, transform.position.z)).normalized;
        Quaternion rot = Quaternion.LookRotation(dir);
        Debug.Log($"from {transform.rotation} to {rot}");
        transform.rotation = Quaternion.Lerp(transform.rotation, rot, rotationSpeed * Time.deltaTime);
        

    }

    private void OnDrawGizmos()
    {
        if (enableDebug)
        {
            Vector3 pos = new Vector3(0, transform.position.y + shootPosition.localPosition.y - size.y * 0.5f, 0);
            Gizmos.color = new Color(Color.blue.r, Color.blue.g, Color.blue.b, 0.5f);
            Gizmos.DrawCube(pos, size);
            Gizmos.color = new Color(Color.red.r, Color.red.g, Color.red.b, 1.0f);
            Gizmos.DrawSphere(center, 0.2f);
        }
    }
}

