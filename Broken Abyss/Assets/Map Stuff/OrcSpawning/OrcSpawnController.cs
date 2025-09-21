using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrcSpawnController : MonoBehaviour
{
    // Start is called before the first frame update
    public float spawnerLevel = 3f;

    // Called by orc spawner
    // Increments global orc spawner level
    // If level is at 45, the difficulty will not increase
    public void incrementLevel()
    {
        if (spawnerLevel < 45){
            spawnerLevel += 2f;
        }
        
    }
    // Returns spawner level
    public float getLevel()
    {
        return spawnerLevel;
    }

}
