using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemTest2 : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        var inventory = GameObject.Find("HeroCanvas/PlayerInventory").GetComponent<Inventory>();

        if(collision.CompareTag("Player"))
        {
            Destroy(gameObject);
            inventory.RemoveItemInventory();
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
