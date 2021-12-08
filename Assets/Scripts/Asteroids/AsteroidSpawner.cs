using System;
using Asteroids;
using UnityEngine;
using Random = UnityEngine.Random;

public class AsteroidSpawner : MonoBehaviour {
    
    private float _currentTime = 2f;
    private const float MAXSpawnTime = 2f;
    private const float MAXSpawnWidth = 9.5f;
    private const float MINSpawnWidth = -9.5f;
    private GameObject _asteroidPrefab;

    private void Awake() {
        _asteroidPrefab = Resources.Load<GameObject>("Prefabs/Enemy");
    }

    private void Update() {
        
        _currentTime -= Time.deltaTime;
        if (!(_currentTime <= 0)) return;
        _currentTime = RandomSpawnTime(MAXSpawnTime);
        SpawnAsteroid();
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
