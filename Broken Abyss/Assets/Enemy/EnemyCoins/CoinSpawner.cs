using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    public GameObject coinPrefab; // Assign the coin prefab in the Inspector
    public int numberOfCoins = 5; // Number of coins to spawn
    public float spawnRadius = 1f; // Radius around the monster to spawn coins

    void Start()
    {
        SpawnCoins(); // Test coin spawning when the scene starts
    }

    public void SpawnCoins()
    {
        for (int i = 0; i < numberOfCoins; i++)
        {
            // Generate a random position within a circle around the monster
            Vector2 randomPosition = Random.insideUnitCircle * spawnRadius;
            Vector3 spawnPosition = new Vector3(transform.position.x + randomPosition.x, transform.position.y + randomPosition.y, 0);

            // Instantiate a new coin at the randomized position
            Instantiate(coinPrefab, spawnPosition, Quaternion.identity);
        }
    }
}
