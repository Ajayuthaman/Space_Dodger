using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    public GameObject[] asteroidPrefabs;
    public float initialSpawnRate = 1.5f;
    public float spawnDelay = 1.0f; // Initial delay before spawning starts
    public float initialSpawnSpeed = 5.0f;
    public float spawnHeight = 5.0f; // The vertical range in which asteroids can spawn
    public float minRotationSpeed = 10.0f;
    public float maxRotationSpeed = 50.0f;
    public float asteroidLifetime = 5.0f; // Time in seconds before the asteroid gets destroyed

    // Variables for speed and spawn rate increase
    public float speedIncreaseInterval = 10.0f;
    public float spawnRateIncreaseInterval = 15.0f;
    public float speedIncreaseAmount = 1.0f;
    public float spawnRateDecreaseAmount = 0.1f;

    private float nextSpawnTime;
    private float nextSpeedIncreaseTime;
    private float nextSpawnRateIncreaseTime;

    private float currentSpawnRate;
    private float currentSpawnSpeed;

    void Start()
    {
        currentSpawnRate = initialSpawnRate;
        currentSpawnSpeed = initialSpawnSpeed;
        nextSpawnTime = Time.time + spawnDelay;
        nextSpeedIncreaseTime = Time.time + speedIncreaseInterval;
        nextSpawnRateIncreaseTime = Time.time + spawnRateIncreaseInterval;
    }

    void Update()
    {
        HandleSpawnRateIncrease();
        HandleSpeedIncrease();

        // Check if it's time to spawn a new asteroid
        if (Time.time >= nextSpawnTime)
        {
            SpawnAsteroid();
            nextSpawnTime = Time.time + currentSpawnRate; // Set the next spawn time
        }
    }

    void HandleSpawnRateIncrease()
    {
        if (Time.time >= nextSpawnRateIncreaseTime)
        {
            currentSpawnRate -= spawnRateDecreaseAmount;
            nextSpawnRateIncreaseTime = Time.time + spawnRateIncreaseInterval;
        }
    }

    void HandleSpeedIncrease()
    {
        if (Time.time >= nextSpeedIncreaseTime)
        {
            currentSpawnSpeed += speedIncreaseAmount;
            nextSpeedIncreaseTime = Time.time + speedIncreaseInterval;
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

        // Get the Rigidbody2D component of the new asteroid and move it towards the left with the updated speed
        asteroidRigidbody.velocity = new Vector2(-currentSpawnSpeed, 0);

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
