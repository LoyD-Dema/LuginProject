using UnityEngine;

public class XPMagnet : MonoBehaviour
{
    [SerializeField] private float radius = 2f;

    private void FixedUpdate()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, radius);
        foreach (var hit in hits)
        {
            if (hit.TryGetComponent<XPPickUp>(out var xp))
            {
                xp.BeginAttract(transform.position);
            }
        }
    }
}
