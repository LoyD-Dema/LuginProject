using UnityEngine;
using UnityEngine.Pool;

public class EnemyPool : MonoBehaviour
{
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] [Range(1,100)] private int capacity = 10;
    [SerializeField] [Range(1, 100)] private int maxSize = 50;
    
    private ObjectPool<GameObject> _pool;

    public EnemyPool(GameObject enemyPrefab)
    {
        this._enemyPrefab = enemyPrefab;
        this._pool = new ObjectPool<GameObject>(
            createFunc: CreateItem,
            actionOnGet: OnGetItem,
            actionOnRelease: OnReleaseItem,
            actionOnDestroy: OnDestroyItem,
            collectionCheck: true,
            defaultCapacity: 10,
            maxSize: 100
        );
    }

    private GameObject CreateItem()
    {
        GameObject enemy = Instantiate(_enemyPrefab);
        enemy.name = "Enemy";
        enemy.GetComponent<HealthComponent>().Death += OnEnemyDead;
        enemy.SetActive(false);
        return enemy;
    }
    
    //Get an enemy from the pool
    private void OnGetItem(GameObject enemy)
    {
        enemy.SetActive(true);
    }
    
    private void OnReleaseItem(GameObject enemy)
    {
        enemy.SetActive(false);
    }
    
    private void OnEnemyDead(GameObject enemy)
    {
        _pool.Release(enemy);
        //Other stuff to do on death?
    }

    private void OnDestroyItem(GameObject enemy)
    {
        enemy.GetComponent<HealthComponent>().Death -= OnEnemyDead;
        Destroy(enemy);
    }
}
