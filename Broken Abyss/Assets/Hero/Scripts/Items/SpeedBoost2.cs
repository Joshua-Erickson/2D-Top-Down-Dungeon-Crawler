using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBoost2 : MonoBehaviour
{
    public float boost = 5f;
    public float timer = 0f;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            GameObject player = collision.gameObject;
            PlayerMovement playerScript = player.GetComponent<PlayerMovement>();

            Destroy(gameObject);
            playerScript.moveSpeed += boost;

            //while(timer != 10)
            //{
            //    timer += Time.deltaTime;
            //}

            //playerScript.moveSpeed -= boost;
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
