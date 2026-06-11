using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(BulletBehavior))]
public class IceModifier : MonoBehaviour
{
    [SerializeField] private float .

    BulletBehavior bullet;

    private void Awake()
    {
        bullet = GetComponent<BulletBehavior>();
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
        if (e.Collider.TryGetComponent<IceEffect>(out IceEffect iceEffect))
        {
            iceEffect.enabled = true;
        }
        else
        {
            e.Collider.AddComponent<IceEffect>();
        }
    }

    private void Start()
    {
        bullet.SpeedMultiplayer -= 0.2f;
    }


}
