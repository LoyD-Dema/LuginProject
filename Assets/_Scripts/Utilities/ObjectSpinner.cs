using UnityEngine;

public class ObjectSpinner : MonoBehaviour
{
    [Header("Rotation")]
    [SerializeField]
    private Vector3 axis = Vector3.up;
    [SerializeField]
    private float rotationSpeed = 10;
    [Header("Translation")]
    [SerializeField]
    private float floatingSpeed = 10;
    [SerializeField]
    [Range(0, 1)]
    private float floatingAmplitude = 0.2f;

    private void Update()
    {
        transform.Rotate(axis * rotationSpeed * Time.deltaTime);
        transform.Translate(0, Mathf.Sin(Time.time * floatingSpeed) * floatingAmplitude, 0);
    }
}
