using UnityEngine;
using UnityEngine.InputSystem;
using Utilities;

public class RotateToMouse : MonoBehaviour
{
    [Range(1.0f, 50.0f)]
    [SerializeField] float rotationSpeed = 20.0f;

    private void Update()
    {
        if (MouseInput.GetWorldPositionByMouse(out Vector3 worldPos))
        {
            Vector3 point = new Vector3(worldPos.x, transform.position.y, worldPos.z);
            Vector3 dir = (point - transform.position).normalized;
            Quaternion rot = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Lerp(transform.rotation, rot, rotationSpeed * Time.deltaTime);
        }
    }
}
