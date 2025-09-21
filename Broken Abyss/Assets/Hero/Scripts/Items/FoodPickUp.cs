using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FoodPickUp : MonoBehaviour
{
    private bool hasCollided = false;
    [SerializeField] public string item;
    
    void OnTriggerEnter2D(Collider2D collision)
    {
        var playerInventory = GameObject.Find("HeroCanvas/PlayerInventory").GetComponent<Inventory>();

        if (collision.CompareTag("Player") && !hasCollided && playerInventory.CheckInventorySpace())
        {
            Destroy(gameObject);
            hasCollided = true;
            playerInventory.AddItemInventory(item);
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
