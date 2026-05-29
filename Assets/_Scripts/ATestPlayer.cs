using UnityEngine;

[RequireComponent(typeof(HealthComponent))]
public class ATestPlayer : MonoBehaviour
{
    private HealthComponent hc;
    private void Start()
    {
        hc = GetComponent<HealthComponent>();
    }
}
