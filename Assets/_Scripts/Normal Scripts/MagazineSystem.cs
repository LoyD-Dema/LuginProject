using UnityEngine;



public class MagazineSystem : MonoBehaviour
{
    [SerializeField]
    protected GameObject bulletPrefab;
    [SerializeField]
    protected const ushort maxChambers = 6;

    protected ushort selectedChamber = 0;

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
