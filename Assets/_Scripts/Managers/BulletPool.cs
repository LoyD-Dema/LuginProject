using UnityEngine;
using UnityEngine.Pool;

public class BulletPool : MonoBehaviour
{
    [SerializeField] private BulletBehavior bulletPrefab;
    [SerializeField][Range(1, 100)] private int capacity = 12;
    [SerializeField][Range(1, 100)] private int maxSize = 24;

    private ObjectPool<BulletBehavior> pool;

    private void Awake()
    {
        pool = new ObjectPool<BulletBehavior>(
            createFunc: CreateItem,
            actionOnGet: GetItem,
            actionOnRelease: OnReleaseItem,
            actionOnDestroy: OnDestroyItem,
            collectionCheck: true,
            defaultCapacity: capacity,
            maxSize: maxSize
        );
    }

    private void Start()
    {
        CreateItem();
    }

    private BulletBehavior CreateItem()
    {
        BulletBehavior bullet = Instantiate(bulletPrefab);
        bullet.name = "Bullet";
        bullet.gameObject.SetActive(false);
        return bullet;
    }

    public BulletBehavior GetItem()
    {
        return pool.Get();
    }

    private void GetItem(BulletBehavior bullet)
    {
        bullet.gameObject.SetActive(true);
    }

    private void OnReleaseItem(BulletBehavior bullet)
    {
        bullet.gameObject.SetActive(false);
    }

    private void OnDestroyItem(BulletBehavior bullet)
    {
        Destroy(bullet.gameObject);
    }
}
