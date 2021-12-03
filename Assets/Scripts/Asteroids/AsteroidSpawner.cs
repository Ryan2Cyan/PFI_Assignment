using UnityEngine;
using Random = UnityEngine.Random;

public class AsteroidSpawner : MonoBehaviour {
    
    [SerializeField] private const GameObject AsteroidPrefab = null;
    [SerializeField] private float currentTime;
    [SerializeField] private const float MINSpawnTime = 0.2f;
    [SerializeField] private const float MAXSpawnTime = 1.5f;
    [SerializeField] private const float MAXSpawnWidth = 1.5f;
    [SerializeField] private const float MINSpawnWidth = 0.2f;

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
        // Here we need to generate which asteroid type we want to spawn!
        var newAsteroid = Instantiate(AsteroidPrefab, transform, true);
        newAsteroid.transform.localPosition = new Vector3(0f, 0f, Random.Range(MINSpawnWidth, MAXSpawnWidth));
    }
}
