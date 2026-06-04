using UnityEngine;
public class ShootComponent : MonoBehaviour
{
    [Range(0.1f, 5.0f)]
    [SerializeField] float fireRate = 0.5f;
    [Range(0f, 5.0f)]
    [SerializeField] float distanceMultiplayer;
    private float elapsedFireRateTime;

    // TODO - Change with the BulletBehavior Class
    [SerializeField] GameObject bullet;

    private void Start()
    {
        elapsedFireRateTime = 0;
    }

    public void Shoot()
    {
        if (elapsedFireRateTime >= 0) return;

        // TODO - Change it by taking the bullet from the pool
        Instantiate(bullet, transform.position + transform.forward * distanceMultiplayer, transform.rotation);

        Debug.Log("Shot");
        elapsedFireRateTime = fireRate;
    }

    private void Update()
    {
        elapsedFireRateTime -= Time.deltaTime;
    }
}
