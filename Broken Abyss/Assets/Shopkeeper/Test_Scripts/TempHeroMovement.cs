using UnityEngine;

public class TempHeroMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public int gold = 100;  // Player's current gold amount

    public HotBar hotbar;  // Reference to the Hotbar to add items to
    public Item selectedItem;  // The item the player is attempting to buy

    void Update()
    {
        // Movement logic
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");

        Vector2 move = new Vector2(moveX, moveY) * moveSpeed * Time.deltaTime;
        transform.Translate(move);
    }

    // Method to handle buying an item
    public bool BuyItem(Item item)
    {
        if (item != null)
        {
            if (gold >= item.price)
            {
                gold -= item.price;
                hotbar.AddItemToHotbar(item);  // Add item to hotbar
                Debug.Log("Bought: " + item.itemName);
                return true;
            }
            else
            {
                Debug.Log("Not enough gold to buy " + item.itemName);
                return false;
            }
        }
        return false;
    }
}
