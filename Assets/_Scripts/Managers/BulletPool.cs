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
            actionOnGet: OnGet,
            actionOnRelease: OnReleaseItem,
            actionOnDestroy: OnDestroyItem,
            collectionCheck: true,
            defaultCapacity: capacity,
            maxSize: maxSize
        );
    }

    private void Start()
    {
        pool.Release(CreateItem());
    }

    private BulletBehavior CreateItem()
    {
        BulletBehavior bullet = Instantiate(bulletPrefab);
        bullet.name = "Bullet";
        bullet.gameObject.SetActive(false);
        bullet.Pool = this;
        return bullet;
    }

    public BulletBehavior Get()
    {
        return pool.Get();
    }
    public void Relese(BulletBehavior bullet)
    {
        if(!bullet.bIsReleased)
            pool.Release(bullet);
    }

    private void OnGet(BulletBehavior bullet)
    {
        bullet.gameObject.SetActive(true);
    }

    private void OnReleaseItem(BulletBehavior bullet)
    {
        bullet.gameObject.SetActive(false);
    }

    private void OnDestroyItem(BulletBehavior bullet)
    {
        Debug.Break();
        Destroy(bullet.gameObject);
    }
}
