using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Pool;
using Utilities;

public class EnemyPool : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField][Range(1, 100)] private int capacity = 10;
    [SerializeField][Range(1, 100)] private int maxSize = 50;
    [SerializeField][Range(1, 100)] private int spawnRateS = 5;
    
    [Header("SpawnDistances")]
    [SerializeField] private Transform playerTransform;
    [SerializeField] private float spawnDistance = 25;
    
    private ObjectPool<GameObject> pool;

    private void Awake()
    {
        pool = Pools.CreatePool(
            "outlawPool",
            enemyPrefab,
            createFunc: CreateItem,
            onGet: e => e.SetActive(true),
            onRelease: OnRelease,
            onDestroy: e=> Destroy(e),
            capacity,
            maxSize
        );
    }

    private void OnRelease(GameObject obj)
    {
        obj.SetActive(false);
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
                if (TryGetValidNavMeshSpawnPoint(out Vector3 finalSpawnPosition))
                {
                    GameObject enemy = pool.Get();
                    if (enemy.TryGetComponent<NavMeshAgent>(out NavMeshAgent agent))
                    {
                        agent.enabled = false;
                        agent.Warp(finalSpawnPosition);
                        agent.enabled = true;
                    }
                }
            }
            yield return new WaitForSeconds(interval);
        }
    }

    private bool TryGetValidNavMeshSpawnPoint(out Vector3 finalPosition)
    {
        int maxTry = 10;
        finalPosition = Vector3.zero;

        for (int i = 0; i < maxTry; i++)
        {
            Vector2 randomDirection = Random.insideUnitCircle.normalized;
            Vector3 spawnPointOffset = new Vector3(randomDirection.x, 0, randomDirection.y) * spawnDistance;
            Vector3 rawSpawnPosition = playerTransform.position + spawnPointOffset;

            if (NavMesh.SamplePosition(rawSpawnPosition, out NavMeshHit hit, spawnDistance, NavMesh.AllAreas))
            {
                finalPosition = hit.position;
                return true;
            }
        }
        return false;
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
    
    private void OnEnemyDead(GameObject enemy)
    {
        Debug.Log($"Morto {enemy}", enemy);
        StartCoroutine(ReleaseAfterAnimation(enemy));
        //pool.Release(enemy);
        //Other stuff to do on death?
    }

    private IEnumerator ReleaseAfterAnimation(GameObject enemy)
    {
        if (enemy.TryGetComponent<NavMeshAgent>(out var agent)) agent.enabled = false;
        if (enemy.TryGetComponent<Collider>(out var collider)) collider.enabled = false;
        if (enemy.TryGetComponent<AIController>(out var controller)) controller.enabled = false;
        if (enemy.TryGetComponent<Animator>(out var animator))
        {
            animator.SetTrigger("IsDead");
        }
        yield return new WaitForSeconds(3.2f);

        if (collider != null) collider.enabled = true;
        if (controller != null) controller.enabled = true;
        pool.Release(enemy);
    }

}
