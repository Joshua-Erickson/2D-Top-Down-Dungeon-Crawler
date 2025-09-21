using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.Tilemaps;  // Required to access Tilemap

public class ItemGenerator : MonoBehaviour
{
    // Assign prefabs for items
    public GameObject[] itemPrefabs;
    //public PlayerHealth playerHealth;       // Reference to the player's health
    public Transform player;                // Reference to the player's Transform
    public Tilemap tilemap;  // Reference to the tilemap to constrain item spawning

    private int maxItems = 40;               // Maximum number of items
    private List<GameObject> generatedItems = new List<GameObject>();

    public float itemLifetime = 180f;       // Visibility duration (3 minutes)
    public float spawnRadius = 50f;         // Radius for random spawn locations around the map
    public float activationRadius = 5f;    // Radius within which items become visible to the player

    void Start()
    {
        SpawnItemsAcrossMap();  // Spawn items across the map initially
    }

    void SpawnItemsAcrossMap()
    {
        for (int i = 0; i < maxItems; i++)
        {
            GameObject item = Instantiate(itemPrefabs[Random.Range(0, itemPrefabs.Length)], GetRandomPosition(), Quaternion.identity);
            item.SetActive(false);  // Make the item invisible initially
            generatedItems.Add(item);  // Add to the list of generated items
        }
    }

    void Update()
    {
        CheckPlayerProximity();  // Check distance between player and items each frame
    }

    void CheckPlayerProximity()
    {
        foreach (GameObject item in generatedItems)
        {
            if (item != null && !item.activeSelf)
            {
                float distanceToPlayer = Vector3.Distance(player.position, item.transform.position);

                if (distanceToPlayer <= activationRadius)
                {
                    item.SetActive(true); // Make item visible when within range
                    StartCoroutine(HideItemAfterTime(item));  // Start timer to hide item after visibility duration
                }
            }
        }
    }

    IEnumerator HideItemAfterTime(GameObject item)
    {
        yield return new WaitForSeconds(itemLifetime);
        if (item != null)
        {
            item.SetActive(false); // Hide the item after the specified lifetime
        }
    }

    // Get random position for item placement within the map's spawn radius
    Vector3 GetRandomPosition()
    {
        // Get the bounds of the tilemap
        BoundsInt bounds = tilemap.cellBounds;

        // Generate a random position within the tilemap bounds
        Vector3Int randomCell;

        do
        {
            int randomX = Random.Range(bounds.xMin, bounds.xMax);
            int randomY = Random.Range(bounds.yMin, bounds.yMax);
            randomCell = new Vector3Int(randomX, randomY, 0);

        } while (!tilemap.HasTile(randomCell)); // Ensure the selected tile is not empty (has a tile)

        // Convert the cell position to world position to spawn the item
        Vector3 spawnPosition = tilemap.GetCellCenterWorld(randomCell);
        return spawnPosition;
    }

    // Detects when the player "consumes" an item
    void OnTriggerEnter(Collider other)
    {
        //ItemAttributes foodItem = other.gameObject.GetComponent<ItemAttributes>();
        //if (foodItem != null)
        if (other.CompareTag("Food")) // Ensure the Tag matches
        {
            //foodItem.UseItem(); // Consume the food item to affect health
            GameObject food = other.gameObject;
            if (food.activeSelf) //Ensure the item is visible
            {
                ItemAttributes foodItem = food.GetComponent<ItemAttributes>();
                if (foodItem != null)
                {
                    foodItem.UseItem(); // Consume the food item to effect health
                    Destroy(food); // Remove the item from the map
                }
            }
        }
    }
}
