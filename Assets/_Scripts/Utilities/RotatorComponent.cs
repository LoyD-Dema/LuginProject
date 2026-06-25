using UnityEngine;

public class RotatorComponent : MonoBehaviour
{
    [SerializeField]
    private float rotationSpeed = 10;
    [SerializeField]
    private Vector3 axis = Vector3.up;

    private void Update()
    {
        transform.Rotate(axis * rotationSpeed * Time.deltaTime);
    }
}
