using UnityEngine;

/// <summary>
/// Spawns a number of prefabs representing XP that the player can then pick up.
/// </summary>
public class XPDrop : MonoBehaviour
{
    [SerializeField] private XPPickUp XpPickupPrefab;
    private int numInstances = 5; //the number of entities to drop
    
    internal void Drop(int totXpDropped, Vector3 position)
    {
        Debug.Log($"Dropped {numInstances} gems of XP");
        
        for (int i = 0; i < numInstances; i++)
        {
            var pickup = Instantiate(XpPickupPrefab, position, Quaternion.identity); //this give to a pool.
            pickup.Value = totXpDropped/numInstances;
            
            //Animation
            Vector3 velocity = Random.insideUnitSphere * 10f;
            velocity.y = Mathf.Abs(velocity.y);
            pickup.Launch(velocity);
        }
    }
}
