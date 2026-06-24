using System.Collections;
using UnityEngine;

public class FireEffect : MonoBehaviour
{
    private float effectDuration;
    private float damageInterval;
    private float damageAmount;

    private HealthComponent healthComponent;
    private Coroutine fireCoroutine;

    private void Awake()
    {
        healthComponent = GetComponent<HealthComponent>();

        effectDuration = EffectsManager.I.FireEffectDuration;
        damageInterval = EffectsManager.I.FireDamageInterval;
        damageAmount = EffectsManager.I.FireDamageAmount;
    }

    private void OnEnable()
    {
        ResetEffect();
    }

    private void OnDisable()
    {
        if (fireCoroutine != null)
        {
            StopCoroutine(fireCoroutine);
            fireCoroutine = null;
        }
    }

    public void ResetEffect()
    {
        if (fireCoroutine != null)
        {
            StopCoroutine(fireCoroutine);
        }
        fireCoroutine = StartCoroutine(ApplyBurnDamage());
    }

    private IEnumerator ApplyBurnDamage()
    {
        float elapsedTime = 0f;
        float intervalTimer = 0f;

        while (elapsedTime < effectDuration)
        {
            if (healthComponent != null && healthComponent.IsDead)
                yield break;

            yield return null;
            elapsedTime += Time.deltaTime;
            intervalTimer += Time.deltaTime;

            if (intervalTimer >= damageInterval)
            {
                if (healthComponent != null)
                {
                    healthComponent.TakeDamage(damageAmount);
                }

                intervalTimer -= damageInterval;
            }
        }
        enabled = false;
    }
}
