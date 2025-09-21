using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemTest : MonoBehaviour
{
    private bool hasCollided = false;
    [SerializeField] private string foodName;
    
    void OnTriggerEnter2D(Collider2D collision)
    {
        var inventory = GameObject.Find("HeroCanvas/PlayerInventory").GetComponent<Inventory>();

        if(collision.CompareTag("Player") && !hasCollided && inventory.CheckInventorySpace())
        {
            Destroy(gameObject);
            hasCollided = true;
            inventory.AddItemInventory(foodName);
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
