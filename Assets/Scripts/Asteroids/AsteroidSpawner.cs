using System;
using Asteroids;
using UnityEngine;
using Random = UnityEngine.Random;

public class AsteroidSpawner : MonoBehaviour {
    
    [SerializeField] private const GameObject AsteroidPrefab = null;
    [SerializeField] private float currentTime;
    [SerializeField] private const float MINSpawnTime = -1.5f;
    [SerializeField] private const float MAXSpawnTime = 1.5f;
    [SerializeField] private const float MAXSpawnWidth = 9.5f;
    [SerializeField] private const float MINSpawnWidth = 0f;

    private void Update() {
        
        currentTime -= Time.deltaTime;
        if (currentTime <= 0) {
            currentTime = RandomSpawnTime(MINSpawnTime, MAXSpawnTime);
            SpawnAsteroid();
        }
    }

    // Randomly generates a spawn time for asteroid:
    private static float RandomSpawnTime(float min, float max) {
        return Random.Range(min, max);
    }

    // Spawns an asteroid:
    private void SpawnAsteroid() {
        var newAsteroidType = new Asteroid((AsteroidType)Random.Range(0, 5));
        var go = Instantiate(newAsteroidType.AsteroidPrefab, transform, true);
        go.transform.localScale = newAsteroidType.Scale;
        go.transform.localPosition = new Vector3(0, 0, Random.Range(MINSpawnWidth, MAXSpawnWidth));
    }
}
