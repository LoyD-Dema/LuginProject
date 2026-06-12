using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(BulletBehavior))]
public class IceModifier : MonoBehaviour
{
    private float decreseDmgMultiplayer;

    BulletBehavior bullet;

    private void Awake()
    {
        bullet = GetComponent<BulletBehavior>();
        decreseDmgMultiplayer = EffectsManager.I.BulletDmnMultiplayerToDecrese;
    }

    private void OnEnable()
    {
        bullet.OnHit += Bullet_OnHit;
    }

    private void OnDisable()
    {
        bullet.OnHit -= Bullet_OnHit;
    }

    private void Bullet_OnHit(object sender, OnHitEventArgs e)
    {
        if (!e.Collider.gameObject.CompareTag("Enemy"))
            return;

        if (e.Collider.TryGetComponent<IceEffect>(out IceEffect iceEffect))
        {
            if(iceEffect.enabled)
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

    private void Start()
    {
        bullet.DamageMultiplayer -= decreseDmgMultiplayer;
        bullet.IncreaseNumOfObjectToPirce(1); // Magic number per aumnetare quanto oggeti puo' fare il pirce
    }


}
