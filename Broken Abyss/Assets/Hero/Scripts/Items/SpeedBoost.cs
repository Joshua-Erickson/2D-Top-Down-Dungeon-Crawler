using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBoost : MonoBehaviour
{
    public bool hasCollided = false;
    //public float boost = 2.5f;
    //private float timer = 0f;
    //private float duration = 2f;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            var playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();

            Destroy(gameObject);
            hasCollided = true;
            playerMovement.SpeedPotion();

            //timer += Time.deltaTime;
            //if(timer >= duration)
            //{
            //    timer = 0;
            //    playerScript.moveSpeed -= boost;
            //}
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
