public class ShopItem
{
    public string itemID;
    public int price; // Add a price field

    // Constructor that accepts item name and price
    public ShopItem(string itemName, int itemPrice)
    {
        itemID = itemName;
        price = itemPrice;
    }
}
