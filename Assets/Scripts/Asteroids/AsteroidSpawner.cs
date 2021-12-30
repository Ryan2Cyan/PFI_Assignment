using Game;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Asteroids {
    public class AsteroidSpawner : MonoBehaviour {
    
        private float _currentTime = 2f;
        private float _spawnTime;
        private const float StartSpawnTime = 2f;
        private const float MAXSpawnWidth = 10f;
        private const float MINSpawnWidth = -9.5f;
        private GameObject _asteroidPrefab;

        private void Awake() {
            _asteroidPrefab = Resources.Load<GameObject>("Prefabs/Enemy");
        }

        private void Update() {
        
            // Increase spawn time of asteroids on higher rounds
            _spawnTime = Mathf.Clamp(0.3f, StartSpawnTime - GameController.Round * 0.05f, _spawnTime);

            // Spawn Asteroid:
            if (_spawnTime > 0) {
                _currentTime -= Time.deltaTime;
                if (!(_currentTime <= 0)) return;
                _currentTime = RandomSpawnTime(_spawnTime);
                SpawnAsteroid();
            }
            else {
                SpawnAsteroid();
            }
        }

        // Randomly generates a spawn time for asteroid:
        private static float RandomSpawnTime(float max) {
            return Random.Range(0, max);
        }

        // Spawns an asteroid:
        private void SpawnAsteroid() {
            var go = Instantiate(_asteroidPrefab, transform, true);
            go.transform.localPosition = new Vector3(0, 0, Random.Range(MINSpawnWidth, MAXSpawnWidth));
        }
    }
}
