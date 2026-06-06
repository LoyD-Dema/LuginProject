using UnityEngine;

public class Billboard : MonoBehaviour
{
    private Transform cameraTransform;

    private void OnEnable()
    {
        if (!Camera.main) return;
        cameraTransform = Camera.main.transform;
    }

    private void LateUpdate()
    {
        if (!cameraTransform) return;

        transform.LookAt(transform.position + cameraTransform.forward);
    }
}
