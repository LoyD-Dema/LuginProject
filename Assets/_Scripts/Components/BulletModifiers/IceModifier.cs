using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(BulletBehavior))]
public class IceModifier : BaseModifier
{
    protected override void Awake()
    {
        base.Awake();
        enabled = false;

    }

    private void Start()
    {
        bullet.DamageMultiplayer -= amountDamageMultiplier;
        bullet.IncreaseNumOfObjectToPirce(1); // Magic number per aumnetare quanto oggeti puo' fare il pirce, Usato solo x Test
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        Debug.Log("CIOAIOPIFJPOAJFOPIJPOFJFOPJFPOJPO");
    }

    protected override void Bullet_OnHitTrigger(object sender, OnHitEventArgs e)
    {
        base.Bullet_OnHitTrigger(sender, e);

        if (!e.Collider.gameObject.CompareTag("Enemy"))
            return;

        if (e.Collider.TryGetComponent<IceEffect>(out IceEffect iceEffect))
        {
            if (iceEffect.enabled)
            {
                iceEffect.Reset();
            }
            else
            {
                iceEffect.enabled = true;
            }
        }
        else
        {
            IceEffect effect = e.Collider.AddComponent<IceEffect>();
            effect.enabled = true;
        }
    }
}
