using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

[System.Serializable]
public class Item
{
    public string itemName;    // Name of the item
    public int price;          // Price of the item
    public Sprite itemSprite;  // Sprite representing the item icon
}

public class HotBar : MonoBehaviour
{
    [SerializeField]
    private List<Image> hotbarSlots;  // Reference to the UI slots in the hotbar, now serialized

    public List<Item> itemsInHotbar = new List<Item>(); // The items in the player's hotbar

    void Start()
    {
        // Make sure the hotbar is empty at the start
        foreach (var slot in hotbarSlots)
        {
            slot.enabled = false;  // Hide empty slots initially
        }
    }

    // Method to add an item to the hotbar
    public void AddItemToHotbar(Item newItem)
    {
        for (int i = 0; i < hotbarSlots.Count; i++)
        {
            if (itemsInHotbar.Count < hotbarSlots.Count)
            {
                itemsInHotbar.Add(newItem);
                hotbarSlots[i].sprite = newItem.itemSprite;  // Assign the item sprite to the slot
                hotbarSlots[i].enabled = true;  // Show the slot
                Debug.Log("Added " + newItem.itemName + " to the hotbar.");
                break;
            }
        }
    }
}

