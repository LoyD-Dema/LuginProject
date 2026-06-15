using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace Utilities
{
    /// <summary>
    /// Class Pools provides a collection of pools and ways to instantiate a pool
    /// </summary>
    public static class Pools
    {
        public static Dictionary<string, object> pools = new();
        
        public static ObjectPool<T> CreatePool<T>(
            string id,
            T prefab,
            Func<T> createFunc,
            Action<T> onGet = null,
            Action<T> onRelease = null,
            Action<T> onDestroy = null,
            int capacity = 10,
            int size = 100) where T : UnityEngine.Object
        {
            var pool = new ObjectPool<T>(
                createFunc: createFunc,
                actionOnGet: onGet,
                actionOnRelease: onRelease,
                actionOnDestroy: onDestroy,
                collectionCheck: true,
                defaultCapacity: capacity,
                maxSize: size
            );

            pools[id] = pool;
            return pool;
        }
        
        public static ObjectPool<T> CreateComponentPool<T>(string id, T prefab, int capacity, int size) where T : Component
        {
            var newPool = new ObjectPool<T>(
                createFunc: () => UnityEngine.Object.Instantiate(prefab),
                actionOnGet: item => item.gameObject.SetActive(true),
                actionOnRelease:  item => item.gameObject.SetActive(false),
                actionOnDestroy: item => UnityEngine.Object.Destroy(item.gameObject),
                collectionCheck: true,
                defaultCapacity: capacity,
                maxSize: size
            );
            
            pools[id] = newPool;
            return newPool;
        }
        
        public static ObjectPool<GameObject> CreateGameObjectPool<T>(string id, GameObject prefab, int capacity, int size)
        {
            var newPool = new ObjectPool<GameObject>(
                createFunc: () => UnityEngine.Object.Instantiate(prefab),
                actionOnGet: item => item.SetActive(true),
                actionOnRelease:  item => item.SetActive(false),
                actionOnDestroy: item => UnityEngine.Object.Destroy(item.gameObject),
                collectionCheck: true,
                defaultCapacity: capacity,
                maxSize: size
            );
            
            pools[id] = newPool;
            return newPool;
        }

        public static ObjectPool<T> GetComponentPool<T>(string poolId) where T : Component
        {
            return (ObjectPool<T>)pools[poolId];
        }

        public static void RemovePool(string poolId)
        {
            pools.Remove(poolId);
        }
    }
}