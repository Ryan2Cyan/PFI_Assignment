using System;
using Asteroids;
using UnityEngine;
using Random = UnityEngine.Random;

public class AsteroidSpawner : MonoBehaviour {
    
    private float currentTime = 2f;
    private const float MINSpawnTime = -1.5f;
    private const float MAXSpawnTime = 1.5f;
    private const float MAXSpawnWidth = 9.5f;
    private const float MINSpawnWidth = -9.5f;
    private GameObject _asteroidPrefab;

    private void Awake() {
        _asteroidPrefab = Resources.Load<GameObject>("Prefabs/Enemy");
    }

    private void Update() {
        
        currentTime -= Time.deltaTime;
        if (!(currentTime <= 0)) return;
        currentTime = RandomSpawnTime(MINSpawnTime, MAXSpawnTime);
        SpawnAsteroid();
    }

    // Randomly generates a spawn time for asteroid:
    private static float RandomSpawnTime(float min, float max) {
        return Random.Range(min, max);
    }

    // Spawns an asteroid:
    private void SpawnAsteroid() {
        var go = Instantiate(_asteroidPrefab);
        go.transform.SetParent(transform);
        go.transform.localPosition = new Vector3(0, 0, Random.Range(MINSpawnWidth, MAXSpawnWidth));
    }
}
