using UnityEngine;
using UnityEngine.UI;

public class SlotClickHandler : MonoBehaviour
{
    public GameObject ItemMenu;  // Reference to the item options menu (Buy/Item Lore)
    public string itemName;      // Name of the item in this slot
    public int itemPrice;        // Price of the item in this slot
    private ShopkeeperUI shopkeeperUI;  // Reference to ShopkeeperUI

    void Start()
    {
        // Ensure the ItemMenu is hidden at the start
        ItemMenu.SetActive(false);

        // Find the ShopkeeperUI component in the scene (make sure it's active)
        shopkeeperUI = FindObjectOfType<ShopkeeperUI>();
    }

    // Triggered when the slot is clicked
    public void OnSlotClick()
    {
        // Make the ItemMenu visible
        ItemMenu.SetActive(true);
        Debug.Log($"Item slot clicked. ItemMenu is now visible. Item: {itemName}, Price: {itemPrice}");

        // Set the selected item in ShopkeeperUI with price
        if (shopkeeperUI != null)
        {
            shopkeeperUI.selectedItem = new ShopItem(itemName, itemPrice); // Use shopkeeperUI to set the selected item
            shopkeeperUI.OnSlotClicked(itemName, itemPrice); // Call the OnSlotClicked method in ShopkeeperUI with itemName and price
        }
    }
}
