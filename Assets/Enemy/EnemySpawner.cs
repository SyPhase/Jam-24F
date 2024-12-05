using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] float maxTimeBetweenSpawn = 10f;
    [SerializeField] float minTimeBetweenSpawn = 1f;
    [SerializeField] float spawnAcceleration = 0.2f;
    [SerializeField] float startDelay = 0f;
    float timeBetweenSpawn;
    float lastSpawnTime;

    void Start()
    {
        timeBetweenSpawn = maxTimeBetweenSpawn;
        lastSpawnTime = -startDelay;
    }

    void FixedUpdate()
    {
        lastSpawnTime += Time.fixedDeltaTime; // Add to timer

        if (lastSpawnTime > timeBetweenSpawn) // if time is more than the time needed between each spawn
        {
            lastSpawnTime = 0f; // Reset Time
            Instantiate(enemyPrefab, transform.position, transform.rotation, transform); // Spawn Enemy

            timeBetweenSpawn -= spawnAcceleration; // Reduce delay between spawns
            if (timeBetweenSpawn < minTimeBetweenSpawn) // Ensure a minimun time between spawns
            {
                timeBetweenSpawn = minTimeBetweenSpawn;
            }
        }
    }
}
