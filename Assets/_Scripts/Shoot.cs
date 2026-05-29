using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class Shoot : MonoBehaviour
{
    [Range(0.1f, 5.0f)]
    [SerializeField] float fireRate = 0.5f;
    [Range(1.0f, 5.0f)]
    [SerializeField] float distanceMultiplayer;
    private float elapsedFireRateTime;

    // TODO - Change with the BulletBehavior Class
    [SerializeField] GameObject bullet;

    private void Start()
    {
        elapsedFireRateTime = 0;
    }

    private void Update()
    {
        elapsedFireRateTime -= Time.deltaTime;
    }

    private void OnShoot(InputValue value)
    {
        if (elapsedFireRateTime <= 0)
        {
            // TODO - Change it by taking the bullet from the pool
            Instantiate(bullet, transform.position + transform.forward * distanceMultiplayer, Quaternion.identity);
                                            
            Debug.Log("Shooted");
            elapsedFireRateTime = fireRate;
        }
        else
        {
            Debug.Log("Cant shoot, you have to wait: " + elapsedFireRateTime + " seconds");
        }
    }
}
