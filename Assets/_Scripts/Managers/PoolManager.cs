using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace Managers
{
    /// <summary>
    /// Allow for different systems to share a common pool manager.
    /// </summary>
    public class PoolManager
    {
        private readonly Dictionary<Type, object> _pools = new();
        
        public void RegisterPool<T>(ObjectPool<T> pool) where T : Component
        {
            _pools[typeof(T)] = pool;
        }

        public ObjectPool<T> GetPool<T>() where T : UnityEngine.Component
        {
            return (ObjectPool<T>)_pools[typeof(T)];
        }
    }
}
