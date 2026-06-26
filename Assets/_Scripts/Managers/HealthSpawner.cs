using System.Collections;
using UnityEngine;

public class HealthSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject healthPickup;
    [SerializeField]
    private float minSpawnRate = 1; // Per minute
    [SerializeField]
    private float maxSpawnRate = 2;
    [SerializeField]
    [Range(0, 1)]
    private float randomness = 1;
    [SerializeField]
    private float spawnRadius;

    private void Start()
    {
        StartCoroutine(SpawnHealth());
    }

    private void OnDestroy()
    {
        StopCoroutine(SpawnHealth());
    }

    private IEnumerator SpawnHealth()
    {
        while (true)
        {
            float spawnTime = Random.Range(minSpawnRate, Mathf.Max(minSpawnRate, maxSpawnRate * randomness)) * 60;
            Vector3 randomPointInCircle = Random.insideUnitSphere * spawnRadius;
            Vector3 spawnPosition = new Vector3(transform.position.x + randomPointInCircle.x, transform.position.y + 1, transform.position.z + randomPointInCircle.z);
            Debug.Log($"Next heart in {spawnTime} seconds");
            yield return new WaitForSeconds(spawnTime);
            Instantiate(healthPickup, spawnPosition, Quaternion.identity);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, spawnRadius);
    }
}
