using UnityEngine;

public class MagazineSystem : MonoBehaviour
{
    [SerializeField]
    protected GameObject bulletPrefab;
    [SerializeField]
    protected const int maxChambers = 6;

    protected int selectedChamber = 0;

    public virtual GameObject GetBullet()
    {
        GameObject bullet = Instantiate(bulletPrefab); // CHANGE WITH BULLET POOL

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
