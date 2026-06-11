using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(BulletBehavior))]
public class IceModifier : MonoBehaviour
{
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
        //e.Collider.AddComponent<>();
    }

    private void Start()
    {
        bullet.SpeedMultiplayer -= 0.2f;
    }


}
