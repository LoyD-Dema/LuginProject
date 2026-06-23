using System.Collections;
using UnityEngine;

public class FireEffect : MonoBehaviour
{
    private float effectDuration = 5.0f;
    private float damageInterval = 1.0f;
    private float damageAmount = 10f;

    private HealthComponent healthComponent;
    private Coroutine fireCoroutine;

    private void Awake()
    {
        healthComponent = GetComponent<HealthComponent>();
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
