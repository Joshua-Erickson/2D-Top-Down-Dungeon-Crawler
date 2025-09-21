using System.Collections;
using System.Collections.Generic;
//using UnityEditor.SearchService;
using UnityEngine;

public class RandomObstacles : MonoBehaviour
{
    public GameObject[] obstacles;
    // Start is called before the first frame update
    void Start()
    {
        int rand = Random.Range(0,obstacles.Length);
        Instantiate(obstacles[rand], transform.position, Quaternion.identity);
    }
}
