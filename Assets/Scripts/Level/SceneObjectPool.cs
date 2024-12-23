using Level.SpawnableObjects;
using UnityEngine;
using UnityEngine.Pool;

namespace Level
{
    public class SceneObjectPool
    {
        private const int DefaultCapacity = 10;
        private const int MaxSize = 30;

        private readonly ObjectPool<SpawnableObject> _pool;
        private readonly GameObject _prefab;

        public SceneObjectPool(GameObject prefab)
        {
            _prefab = prefab;
            _pool = new ObjectPool<SpawnableObject>(OnCreateGameObject, OnGetGameObject, OnReleaseGameObject,
                OnDestroyGameObject, false, DefaultCapacity, MaxSize);
        }

        public SpawnableObject Get() => _pool.Get();

        public void Release(SpawnableObject spawnableObject) => _pool.Release(spawnableObject);

        private SpawnableObject OnCreateGameObject() => Object.Instantiate(_prefab).GetComponent<SpawnableObject>();

        private void OnGetGameObject(SpawnableObject spawnableObject) => spawnableObject.gameObject.SetActive(true);
        private void OnReleaseGameObject(SpawnableObject spawnableObject) => spawnableObject.gameObject.SetActive(false);
        private void OnDestroyGameObject(SpawnableObject spawnableObject) => Object.Destroy(spawnableObject);
    }
}