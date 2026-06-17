using UnityEngine;
using UnityEngine.Pool;
using Utilities;

/// <summary>
/// Spawns a number of prefabs representing XP that the player can then pick up.
/// </summary>
public class XPDrop : MonoBehaviour
{
    [SerializeField] private XPPickUp XpPickupPrefab;
    [SerializeField] [Range(1,10)] private int numInstances = 5; //the number of entities to drop

    internal ObjectPool<XPPickUp> xpDroppablesPool;

    private void Start()
    {
        xpDroppablesPool = Pools.CreatePool(
            "droppables",
            XpPickupPrefab, 
            createFunc: () => Object.Instantiate(XpPickupPrefab),
            onGet: e=> e.gameObject.SetActive(true),
            onRelease: e=> e.gameObject.SetActive(false),
            onDestroy: e => UnityEngine.Object.Destroy(e.gameObject),
            200,
            500
        );
    }
    
    internal void Drop(int totXpDropped, Vector3 position)
    {
        for (int i = 0; i < numInstances; i++)
        {
            var pickup = xpDroppablesPool.Get();
            //Randomize a bit the spawn position
            Vector2 offset = Random.insideUnitCircle * .5f;
            pickup.transform.position = position + new Vector3(offset.x, 0f, offset.y);
            pickup.Value = totXpDropped/numInstances;
            
            //Animation
            Vector3 velocity = Vector3.up * 10f + new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f)) * Random.Range(2f, 5f);
            pickup.Launch(this,velocity);
        }
    }

    internal void Destroy(XPPickUp obj)
    {
        xpDroppablesPool.Release(obj);
    }
}
