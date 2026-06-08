using UnityEngine;
using UnityEngine.InputSystem;

public class HealTest : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float healAmount = 20f;

    private HealthComponent healthComponent;

    private void Awake()
    {
        healthComponent = GetComponent<HealthComponent>();
    }

    private void OnJump(InputValue value)
    {
        if (value.isPressed)
        {
            if (healthComponent != null)
            {
                healthComponent.GainHealth(healAmount);
            }
        }
    }
}
