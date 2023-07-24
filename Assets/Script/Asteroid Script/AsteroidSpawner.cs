using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    public GameObject[] asteroidPrefabs; 
    public float spawnRate = 1.5f; 
    public float spawnDelay = 1.0f; // Initial delay before spawning starts
    public float spawnSpeed = 5.0f; 
    public float spawnHeight = 5.0f; // The vertical range in which asteroids can spawn
    public float minRotationSpeed = 10.0f; 
    public float maxRotationSpeed = 50.0f; 
    public float asteroidLifetime = 5.0f; // Time in seconds before the asteroid gets destroyed

    private float nextSpawnTime;

    void Start()
    {
        // Set the next spawn time after the initial delay
        nextSpawnTime = Time.time + spawnDelay;
    }

    void Update()
    {
        // Check if it's time to spawn a new asteroid
        if (Time.time >= nextSpawnTime)
        {
            SpawnAsteroid();
            nextSpawnTime = Time.time + spawnRate; // Set the next spawn time
        }
    }

    void SpawnAsteroid()
    {
        // Calculate the random Y position for the asteroid to spawn
        float randomYPosition = Random.Range(-spawnHeight, spawnHeight);

        // Randomly select an asteroid prefab from the array
        GameObject asteroidPrefab = asteroidPrefabs[Random.Range(0, asteroidPrefabs.Length)];

        // Create a new asteroid at the specified position and rotation
        GameObject newAsteroid = Instantiate(asteroidPrefab, new Vector3(transform.position.x, randomYPosition, 0), Quaternion.identity);

        // Apply random rotation to the asteroid
        float randomRotationSpeed = Random.Range(minRotationSpeed, maxRotationSpeed);
        Rigidbody2D asteroidRigidbody = newAsteroid.GetComponent<Rigidbody2D>();
        asteroidRigidbody.angularVelocity = Random.Range(0, 2) == 0 ? randomRotationSpeed : -randomRotationSpeed;

        // Get the Rigidbody2D component of the new asteroid and move it towards the left
        asteroidRigidbody.velocity = new Vector2(-spawnSpeed, 0);

        // Start the coroutine to destroy the asteroid after a certain time
        StartCoroutine(DestroyAsteroidAfterDelay(newAsteroid, asteroidLifetime));
    }

    IEnumerator DestroyAsteroidAfterDelay(GameObject asteroid, float delay)
    {
        // Wait for the specified delay
        yield return new WaitForSeconds(delay);

        // Destroy the asteroid after the delay
        Destroy(asteroid);
    }
}
