namespace Utilities
{
    public class PrefabFactory<T> : IFactory <T> where T : UnityEngine.Component
    {
        private readonly T _prefab;

        public PrefabFactory(T prefab)
        {
            _prefab = prefab;
        }
        
        public T Create()
        {
            return UnityEngine.Object.Instantiate(_prefab);
        }
    }
}