using UnityEngine;

public class DestroyOutOfRange : MonoBehaviour
{
    [SerializeField] private float range;

    private void Update()
    {
        if (transform.position.magnitude > range)
        {
            Destroy(gameObject);
        }
    }
}
