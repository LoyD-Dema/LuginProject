using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarUnderPlayer : MonoBehaviour
{
    [SerializeField] HealthComponent healthComponent;
    [SerializeField] Image healthFillImage;
    void Awake()
    {
        //Debug
        if (healthComponent == null)
        {
            Debug.LogError($"Manca HealthComponent in {gameObject.name}");
        }
    }

    private void OnEnable()
    {
        if (!healthComponent) return;
        healthComponent.Damage += OnHealthChange;
        healthComponent.Heal += OnHealthChange;
    }

    private void OnDisable()
    {
        if (!healthComponent) return;
        healthComponent.Damage -= OnHealthChange;
        healthComponent.Heal -= OnHealthChange;
    }

    private void Start()
    {
        OnHealthChange();
    }

    private void OnHealthChange()
    {
        if (!healthComponent || !healthFillImage) return;
        
        if (healthComponent.MaxHealth <= 0) return;

        float health = healthComponent.CurrentHealth / healthComponent.MaxHealth;

        healthFillImage.fillAmount = health;

    }
}
