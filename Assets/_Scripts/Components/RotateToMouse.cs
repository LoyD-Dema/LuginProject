using UnityEngine;
using Utilities;

//TODO: move this in the PlayerController?
public class RotateToMouse : MonoBehaviour
{
    [Range(1.0f, 50.0f)]
    [SerializeField] float rotationSpeed = 20.0f;
    [SerializeField] float zDistFromTransformPos;

    private void Update()
    {
        if (MouseInput.GetWorldPositionByMouse(out Vector3 worldPos))
        {
            Ray ray = Camera.main.ScreenPointToRay(MouseInput.GetMousePositon());
            Plane p = new Plane(Vector3.up, new Vector3(0, transform.position.y + zDistFromTransformPos, 0));

            Vector3 newPointOnPlane = worldPos;
            if (p.Raycast(ray, out float enter))
            {
                newPointOnPlane = ray.GetPoint(enter);
            }

            Vector3 dir = (newPointOnPlane - new Vector3(transform.position.x, newPointOnPlane.y, transform.position.z)).normalized;
            Quaternion rot = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Lerp(transform.rotation, rot, rotationSpeed * Time.deltaTime);
        }
    }
}
