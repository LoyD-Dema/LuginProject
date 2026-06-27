using UnityEngine;

/// <summary>
/// Class to manage the enemy magazine
/// </summary>
public class MagazineSystem : MonoBehaviour
{
    [SerializeField]
    protected GameObject bulletPrefab;
    [SerializeField]
    protected const int maxChambers = 6;
    [SerializeField] protected BulletPool bulletPool;

    protected int selectedChamber = 0;

    public virtual BulletBehavior GetBullet()
    {
        BulletBehavior bullet = bulletPool.Get();

        bullet.ResetBulletStats();

        BaseModifier[] modifiers = bullet.GetComponents<BaseModifier>();
        foreach (var m in modifiers)
        {
            m.enabled = false;
        }

        ChangeChamber();

        return bullet;
    }

    protected void ChangeChamber()
    {
        ++selectedChamber;

        if(selectedChamber >= maxChambers)
        {
            selectedChamber = 0;
        }
    }
}
