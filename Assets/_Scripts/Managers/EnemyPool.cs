using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class EnemyPool : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] [Range(1,100)] private int capacity = 10;
    [SerializeField] [Range(1, 100)] private int maxSize = 50;
    [SerializeField] [Range(1, 100)] private int spawnRateS = 5;
    
    
    private ObjectPool<GameObject> pool;

    private void Awake(){
        pool = new ObjectPool<GameObject>(
            createFunc: CreateItem,
            actionOnGet: OnGetItem,
            actionOnRelease: OnReleaseItem,
            actionOnDestroy: OnDestroyItem,
            collectionCheck: true,
            defaultCapacity: 10,
            maxSize: 100
        );
    }
    
    private void Start()
    {
        CreateItem();
        StartCoroutine(SpawnEnemiesAtInterval(spawnRateS));
    }
    
    private IEnumerator SpawnEnemiesAtInterval(float interval)
    {
        Debug.Log("Spawning enemies");
        while (true)
        {
            Vector3 newRandomSpawnPos = Vector3.zero + Random.insideUnitSphere * 10; //TODO: check this why it doesn't spawn in a random point in a radius
            newRandomSpawnPos.y = transform.position.y;
            OnGetItem(pool.Get());
            yield return new WaitForSeconds(interval);
        }
    }
    
    private GameObject CreateItem()
    {
        GameObject enemy = Instantiate(enemyPrefab);
        enemy.name = "Enemy";
        enemy.GetComponent<HealthComponent>().Death += OnEnemyDead;
        enemy.SetActive(false);
        return enemy;
    }
    
    //Get an enemy from the pool
    public void OnGetItem(GameObject enemy)
    {
        enemy.SetActive(true);
    }
    
    private void OnReleaseItem(GameObject enemy)
    {
        enemy.SetActive(false);
    }
    
    private void OnEnemyDead(GameObject enemy)
    {
        pool.Release(enemy);
        //Other stuff to do on death?
    }

    private void OnDestroyItem(GameObject enemy)
    {
        enemy.GetComponent<HealthComponent>().Death -= OnEnemyDead;
        Destroy(enemy);
    }
}
