using System.Collections.Generic;
using Level.SpawnableObjects;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Level
{
    public class LevelGenerator : MonoBehaviour
    {
        private const float UpdateLevelRepeatTime = 1f;
        
        private readonly Queue<SpawnableObject> _spawnedObjects = new();

        [SerializeField] private SpawnableObject[] _spawnableObjects;
        [SerializeField] private float _spawnHeight = 50f;
        [SerializeField] private float _spawnInterval = 10f;
        [SerializeField] private float _despawnHeight = -20f;
        [SerializeField] private float _spawnRangeX = 15f;

        private Player.Player _player;

        private Dictionary<GameObject, SceneObjectPool> _pools;
        private float _nextSpawnY;

        [Inject]
        private void Construct(Player.Player player)
        {
            _player = player;
        }

        private void Start()
        {
            _pools = new Dictionary<GameObject, SceneObjectPool>();
            foreach (var spawnableObject in _spawnableObjects)
                _pools[spawnableObject.gameObject] = new SceneObjectPool(spawnableObject.gameObject);

            _nextSpawnY = _player.transform.position.y + _spawnHeight;
            
            InvokeRepeating(nameof(UpdateLevel), 0f, UpdateLevelRepeatTime);
        }

        private void UpdateLevel()
        {
            GenerateObjects();
            CleanupObjects();
        }

        private void GenerateObjects()
        {
            while (_player.transform.position.y + _spawnHeight > _nextSpawnY)
            {
                var prefabToSpawn = ChoosePrefab();
                if (prefabToSpawn is not null)
                {
                    var randomX = Random.Range(-_spawnRangeX, _spawnRangeX);
                    var spawnPosition = new Vector3(randomX, _nextSpawnY, 0);

                    var pool = _pools[prefabToSpawn];
                    var newObject = pool.Get();
                    
                    newObject.Setup(_player, spawnPosition, () => pool.Release(newObject));

                    _spawnedObjects.Enqueue(newObject);
                }

                _nextSpawnY += _spawnInterval;
            }
        }

        private GameObject ChoosePrefab()
        {
            float totalWeight = 0;
            foreach (var obj in _spawnableObjects)
                totalWeight += obj.SpawnWeight;

            var randomValue = Random.Range(0, totalWeight);

            foreach (var spawnableObject in _spawnableObjects)
            {
                if (randomValue < spawnableObject.SpawnWeight)
                    return spawnableObject.gameObject;

                randomValue -= spawnableObject.SpawnWeight;
            }

            return null;
        }

        private void CleanupObjects()
        {
            while (_spawnedObjects.TryDequeue(out var oldestObject))
            {
                if (oldestObject == null)
                    continue;

                if (oldestObject.transform.position.y < _player.transform.position.y + _despawnHeight)
                {
                    oldestObject.Release();
                }
                else
                {
                    _spawnedObjects.Enqueue(oldestObject);
                    break;
                }
            }
        }
    }
}