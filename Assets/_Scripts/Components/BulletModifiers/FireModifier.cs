using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(BulletBehavior))]
public class FireModifier : BaseModifier
{
    protected override void Awake()
    {
        base.Awake();
        enabled = false;
    }

    protected override void Bullet_OnHitTrigger(object sender, OnHitEventArgs e)
    {
        base.Bullet_OnHitTrigger(sender, e);

        if (!e.Collider.gameObject.CompareTag("Enemy"))
            return;

        AudioManager.PlaySound3D(SoundType.FireBulletImpact, e.Collider.transform.position);

        if (e.Collider.TryGetComponent<FireEffect>(out FireEffect fireEffect))
        {
            if (fireEffect.enabled)
            {
                fireEffect.ResetEffect();
            }
            else
            {
                fireEffect.enabled = true;
            }
        }
        else
        {
            FireEffect effect = e.Collider.AddComponent<FireEffect>();
            effect.enabled = true;
        }
    }
}
