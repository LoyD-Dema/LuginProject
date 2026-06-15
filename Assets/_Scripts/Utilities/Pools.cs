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
        public static Dictionary<string, object> pools = new(); //this is not type safe.
        
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

        public static ObjectPool<T> GetPool<T>(string poolId) where T : Component
        {
            return (ObjectPool<T>)pools[poolId];
        }

        public static void RemovePool(string poolId)
        {
            pools.Remove(poolId);
        }
    }
}