using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Unity.VisualScripting;
//using System.Numerics;

public class OrcSpawner : MonoBehaviour
{
    public DetectionZone detectionzone;
    public GameObject Orc1;
    public GameObject Orc2;
    public GameObject Orc3;

    public OrcSpawnController spawnController;
    public float counter = 0f;
    public float spawnTime = 15f;
    public float orc2spawnLevel = 3f;
    public float orc3spawnLevel = 5f;
    private float orcSpawnNumber = 2;
    public float spawnRadius = 2;

    // Initialize the counter
   void Start()
    {
        counter = 15f;
    }

    // Every frame of game
    void Update()
    {
        // If player is in detection zone, the spawn time has elapsed, and there are no orcs in the detection zone
        if(detectionzone.isNotEmpty() && (counter > spawnTime) && (!detectionzone.isOrcs()))
        {
            // Grab global spawnLevel
            orcSpawnNumber = spawnController.getLevel();
            // Loop until the local orcSpawnNumber is <= 0
            while (orcSpawnNumber > 0)
            {
                // Pick random spawn position within range
                Vector3 spawnPosition = transform.position + Random.insideUnitSphere * spawnRadius;

                // Spawn Level 3 Orc if spawnLevel is high enough
                if (orcSpawnNumber-10f >= 0){
                    Instantiate(Orc3, spawnPosition, Quaternion.identity);
                    orcSpawnNumber -= 10;
                    
                }
                // Spawn Level 2 Orc if spawnLevel is high enough
                else if (orcSpawnNumber-5f >= 0){
                    Instantiate(Orc2, spawnPosition, Quaternion.identity);
                    orcSpawnNumber -= 5;
                    
                }
                else{
                    Instantiate(Orc1, spawnPosition, Quaternion.identity);
                    orcSpawnNumber -= .5f;
                    
                }
                

            }
            counter = 0f;
            // Increment global spawnLevel
            spawnController.incrementLevel();
        }
        // Increment the counter by the time elapsed
        counter += Time.deltaTime;
    }
}
