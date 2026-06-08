using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarUnderPlayer : MonoBehaviour
{
    [SerializeField] HealthComponent healthComponent;

    [Header("Images")]
    [SerializeField] Image healthFillImage;
    [SerializeField] Image visualFillImage;

    [SerializeField] float lerpSpeed = 3.0f;

    private float targetFillAmt = 1.0f;
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
        if (!healthComponent || healthComponent.MaxHealth <= 0) return;

        targetFillAmt = healthComponent.CurrentHealth / healthComponent.MaxHealth;
        healthFillImage.fillAmount = targetFillAmt;
        visualFillImage.fillAmount = targetFillAmt;
    }

    private void Update()
    {
        if (!healthFillImage || !visualFillImage) return;

        healthFillImage.fillAmount = Mathf.Lerp(healthFillImage.fillAmount, targetFillAmt, lerpSpeed * Time.deltaTime);

        if (healthFillImage.fillAmount > targetFillAmt)
        {
            if (Mathf.Abs(healthFillImage.fillAmount - targetFillAmt) < 0.005f) //Ho provato con Mathf.Approximately ma ci mette troppo a sparire
            {
                visualFillImage.fillAmount = targetFillAmt; 
            }
        }
        else
        {
            visualFillImage.fillAmount = targetFillAmt;
        }
    }

    private void OnHealthChange()
    {
        if (!healthComponent || healthComponent.MaxHealth <= 0) return;

        targetFillAmt = healthComponent.CurrentHealth / healthComponent.MaxHealth;
    }
}
