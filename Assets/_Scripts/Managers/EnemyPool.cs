using System.Collections;
using UnityEngine;
using UnityEngine.Pool;
using Utilities;

public class EnemyPool : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField][Range(1, 100)] private int capacity = 10;
    [SerializeField][Range(1, 100)] private int maxSize = 50;
    [SerializeField][Range(1, 100)] private int spawnRateS = 5;

    [Header("SpanwDistances")]
    [SerializeField] private Transform playerTransform;
    [SerializeField] private float spawnDistance = 25;


    private ObjectPool<GameObject> pool;

    private void Awake()
    {
        pool = Pools.CreatePool(
            "outlawPool",
            enemyPrefab,
            createFunc: CreateItem,
            onGet: OnGetItem,
            onRelease: OnReleaseItem,
            onDestroy: OnDestroyItem,
            capacity,
            maxSize
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
            if (playerTransform != null)
            {
                Vector2 randomDirection = Random.insideUnitCircle.normalized; //TODO: check this why it doesn't spawn in a random point in a radius

                Vector3 spawnPoint = new Vector3(randomDirection.x, 0, randomDirection.y) * spawnDistance;

                Vector3 spawnPosition = playerTransform.position + spawnPoint;

                GameObject enemy = pool.Get();
                enemy.transform.position = spawnPosition;

            }
            yield return new WaitForSeconds(interval);
        }
    }

    private void OnEnable()
    {
        HealthComponent.Death += OnEnemyDead;
    }

    private void OnDisable()
    {
        HealthComponent.Death -= OnEnemyDead;
    }

    private GameObject CreateItem()
    {
        GameObject enemy = Instantiate(enemyPrefab);
        enemy.name = "Enemy";
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
        Debug.Log($"Morto {enemy}", enemy);
        pool.Release(enemy);
        //Other stuff to do on death?
    }

    private void OnDestroyItem(GameObject enemy)
    {
        //HealthComponent.Death -= OnEnemyDead;
        Destroy(enemy);
    }
}
