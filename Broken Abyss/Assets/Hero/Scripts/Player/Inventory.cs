using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class Inventory : MonoBehaviour
{
    public string[] InventorySlots = { "0", "0", "0", "0", "0", "0" };
    
    public int activeSlotIndexNum = 0;
    public bool isDagger = true;
    public bool isApple, isPineapple, isWatermelon, isStrawberry,
                isSword, isMagicSword, isLaserSword, isIceSword, isFlameSword,
                isSpeedPotion, isDamagePotion, isInvinciblePotion, isRevivePotion = false;

    private Controls playerControls;

    public string activeItem;

    private ShopkeeperTrigger shopKeeper;
    public Button Slot1, Slot2, Slot3, Slot4, Slot5, Slot6, Slot7, SellButton;

    //public GameObject SellConfirmationPanel_Item1;
    public ShopkeeperUI shopKeeperUI;
    public TMP_Text confirmationText; // Text component for displaying the sell price in the confirmation panel
    public TMP_Text buyPrice;

    private Dictionary<string, int> itemPrices = new Dictionary<string, int>()
    {
        { "Apple", 10 },
        { "Pineapple", 50 },
        { "Watermelon", 30 },
        { "Strawberry", 20 },
        { "Sword", 25 },
        { "Magic Sword", 75 },
        { "Ice Sword", 100 },
        { "Flame Sword", 150 },
        { "Laser Sword", 200 },
        { "Speed Potion", 15 },
        { "Damage Potion", 15 },
        { "Invincible Potion", 50 },
        { "Revive Potion", 75 }
    };

    private void Awake()
    {
        playerControls = new Controls();
    }

    private void Start()
    {
        shopKeeper = GameObject.Find("shopkeeper_working").GetComponent<ShopkeeperTrigger>();
        shopKeeperUI = GameObject.Find("shopkeeper_working").GetComponent<ShopkeeperUI>();
        confirmationText = GameObject.Find("shopkeeper_UI/SellConfirmationPanel_Item1/confirmationText").GetComponent<TextMeshProUGUI>();
        buyPrice = GameObject.Find("shopkeeper_UI/ItemOptionMenu/BuyPrice").GetComponent<TextMeshProUGUI>();

        playerControls.Inventory.Keyboard.performed += ctx => ToggleActiveSlot((int)ctx.ReadValue<float>());

        foreach(Transform inventorySlot in this.transform)
        {
            inventorySlot.GetChild(0).gameObject.SetActive(false);
            
            if(inventorySlot.name != "HotbarSlot1")
            {
                inventorySlot.GetChild(1).gameObject.SetActive(false);
                inventorySlot.GetChild(2).gameObject.SetActive(false);
                inventorySlot.GetChild(3).gameObject.SetActive(false);
                inventorySlot.GetChild(4).gameObject.SetActive(false);
                inventorySlot.GetChild(5).gameObject.SetActive(false);
                inventorySlot.GetChild(6).gameObject.SetActive(false);
                inventorySlot.GetChild(7).gameObject.SetActive(false);
                inventorySlot.GetChild(8).gameObject.SetActive(false);
                inventorySlot.GetChild(9).gameObject.SetActive(false);
                inventorySlot.GetChild(10).gameObject.SetActive(false);
                inventorySlot.GetChild(11).gameObject.SetActive(false);
                inventorySlot.GetChild(12).gameObject.SetActive(false);
                inventorySlot.GetChild(13).gameObject.SetActive(false);
            }
        }
        this.transform.GetChild(0).GetChild(0).gameObject.SetActive(true);

        // Add listener to open SellConfirmationPanel when Slot1 is clicked
        SellButton.onClick.AddListener(() => ShowSellConfirmationPanel());
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    public void ShowBuyPrice()
    {
        int itemPrice = shopKeeperUI.selectedItem.price;
        string itemName = shopKeeperUI.selectedItem.itemID;
        buyPrice.text = $"{itemName} Price: {itemPrice}";
    }

    public void ShowSellConfirmationPanel()
    {
        // Ensure the active item is set correctly before fetching its price
        ChangeActiveItem();

        // Get the price of the active item
        int itemPrice = GetActiveItemPrice();

        Debug.Log($"Setting confirmation text for item: {activeItem} with price: {itemPrice}");

        // Set the confirmation text with the item price
        confirmationText.text = $"Sell this item for {itemPrice} gold?";

        Debug.Log($"Confirmation text set: Sell this item for {itemPrice} gold?");

        //if(SellConfirmationPanel_Item1 != null)
        //{
        //    // Ensure the active item is set correctly before fetching its price
        //    ChangeActiveItem();

        //    // Get the price of the active item
        //    int itemPrice = GetActiveItemPrice();

        //    Debug.Log($"Setting confirmation text for item: {activeItem} with price: {itemPrice}");

        //    // Set the confirmation text with the item price
        //    confirmationText.text = $"Sell this item for {itemPrice} gold?";

        //    Debug.Log($"Confirmation text set: Sell this item for {itemPrice} gold?");
        //}
        //else
        //{
        //    Debug.LogWarning("SellConfirmationPanel is not assigned in the inspector.");
        //}
    }

    public int GetActiveItemPrice()
    {
        // Check if activeItem is not null or empty before fetching the price
        if(string.IsNullOrEmpty(activeItem))
        {
            Debug.LogWarning("Active item is null or empty.");
            return 0;
        }

        // Check if the itemPrices dictionary contains the active item key
        if(itemPrices.ContainsKey(activeItem))
        {
            int price = itemPrices[activeItem];
            Debug.Log($"Price for {activeItem} is {price}.");
            return price;
        }
        else
        {
            Debug.LogWarning($"Item '{activeItem}' not found in itemPrices dictionary.");
            return 0; // Default price if item not found
        }
    }

    public void AddItemInventory(string itemName)
    {
        if(CheckInventorySpace())
        {
            for(int i = 0; i <= 5; i++)
            {
                if(InventorySlots[i] == "0")
                {
                    InventorySlots[i] = itemName;

                    if(itemName == "Apple") { this.transform.GetChild(i + 1).GetChild(1).gameObject.SetActive(true); }
                    if(itemName == "Pineapple") { this.transform.GetChild(i + 1).GetChild(2).gameObject.SetActive(true); }
                    if(itemName == "Watermelon") { this.transform.GetChild(i + 1).GetChild(3).gameObject.SetActive(true); }
                    if(itemName == "Strawberry") { this.transform.GetChild(i + 1).GetChild(4).gameObject.SetActive(true); }
                    if(itemName == "Sword") { this.transform.GetChild(i + 1).GetChild(5).gameObject.SetActive(true); }
                    if(itemName == "Magic Sword") { this.transform.GetChild(i + 1).GetChild(6).gameObject.SetActive(true); }
                    if(itemName == "Speed Potion") { this.transform.GetChild(i + 1).GetChild(7).gameObject.SetActive(true); }
                    if(itemName == "Damage Potion") { this.transform.GetChild(i + 1).GetChild(8).gameObject.SetActive(true); }
                    if(itemName == "Invincible Potion") { this.transform.GetChild(i + 1).GetChild(9).gameObject.SetActive(true); }
                    if(itemName == "Revive Potion") { this.transform.GetChild(i + 1).GetChild(10).gameObject.SetActive(true); }
                    if(itemName == "Laser Sword") { this.transform.GetChild(i + 1).GetChild(11).gameObject.SetActive(true); }
                    if(itemName == "Ice Sword") { this.transform.GetChild(i + 1).GetChild(12).gameObject.SetActive(true); }
                    if(itemName == "Flame Sword") { this.transform.GetChild(i + 1).GetChild(13).gameObject.SetActive(true); }

                    break;
                }
            }
        }
        else
        {
            return;
        }

        ChangeActiveItem();
    }

    public void RemoveItemInventory()
    {
        if(activeSlotIndexNum != 0)
        {
            switch(activeItem)
            {
                case "Apple": isApple = false; this.transform.GetChild(activeSlotIndexNum).GetChild(1).gameObject.SetActive(false); InventorySlots[activeSlotIndexNum - 1] = "0"; break;
                case "Pineapple": isPineapple = false; this.transform.GetChild(activeSlotIndexNum).GetChild(2).gameObject.SetActive(false); InventorySlots[activeSlotIndexNum - 1] = "0"; break;
                case "Watermelon": isWatermelon = false; this.transform.GetChild(activeSlotIndexNum).GetChild(3).gameObject.SetActive(false); InventorySlots[activeSlotIndexNum - 1] = "0"; break;
                case "Strawberry": isStrawberry = false; this.transform.GetChild(activeSlotIndexNum).GetChild(4).gameObject.SetActive(false); InventorySlots[activeSlotIndexNum - 1] = "0"; break;
                case "Sword": isSword = false; this.transform.GetChild(activeSlotIndexNum).GetChild(5).gameObject.SetActive(false); InventorySlots[activeSlotIndexNum - 1] = "0"; break;
                case "Magic Sword": isMagicSword = false; this.transform.GetChild(activeSlotIndexNum).GetChild(6).gameObject.SetActive(false); InventorySlots[activeSlotIndexNum - 1] = "0"; break;
                case "Speed Potion": isSpeedPotion = false; this.transform.GetChild(activeSlotIndexNum).GetChild(7).gameObject.SetActive(false); InventorySlots[activeSlotIndexNum - 1] = "0"; break;
                case "Damage Potion": isDamagePotion = false; this.transform.GetChild(activeSlotIndexNum).GetChild(8).gameObject.SetActive(false); InventorySlots[activeSlotIndexNum - 1] = "0"; break;
                case "Invincible Potion": isInvinciblePotion = false; this.transform.GetChild(activeSlotIndexNum).GetChild(9).gameObject.SetActive(false); InventorySlots[activeSlotIndexNum - 1] = "0"; break;
                case "Revive Potion": isRevivePotion = false; this.transform.GetChild(activeSlotIndexNum).GetChild(10).gameObject.SetActive(false); InventorySlots[activeSlotIndexNum - 1] = "0"; break;
                case "Laser Sword": isLaserSword = false; this.transform.GetChild(activeSlotIndexNum).GetChild(11).gameObject.SetActive(false); InventorySlots[activeSlotIndexNum - 1] = "0"; break;
                case "Ice Sword": isIceSword = false; this.transform.GetChild(activeSlotIndexNum).GetChild(12).gameObject.SetActive(false); InventorySlots[activeSlotIndexNum - 1] = "0"; break;
                case "Flame Sword": isFlameSword = false; this.transform.GetChild(activeSlotIndexNum).GetChild(13).gameObject.SetActive(false); InventorySlots[activeSlotIndexNum - 1] = "0"; break;
            }
        }

        ChangeActiveItem();
    }

    public bool CheckInventorySpace()
    {
        bool openSlot = false;
        
        foreach(string itemSlot in InventorySlots)
        {
            if(itemSlot == "0")
            {
                openSlot = true;
            }
        }

        if(openSlot)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void ChangeActiveItem()
    {
        if(activeSlotIndexNum == 0)
        {
            isDagger = true;
            isSword = false; isMagicSword = false; isLaserSword = false; isIceSword = false; isFlameSword = false;
            isApple = false; isWatermelon = false; isPineapple = false; isStrawberry = false; 
            isSpeedPotion = false; isDamagePotion = false; isRevivePotion = false; isInvinciblePotion = false;
        }

        for(int i = 0; i <=5; i++)
        {
            if((activeSlotIndexNum - 1) == i)
            {
                activeItem = InventorySlots[i];
                
                switch (activeItem)
                {
                    case "0": isApple = false; isPineapple = false; isWatermelon = false; isStrawberry = false; isDagger = false; isSword = false; isMagicSword = false; isSpeedPotion = false; isDamagePotion = false; isInvinciblePotion = false; isRevivePotion = false; isLaserSword = false; isIceSword = false; isFlameSword = false; break;
                    case "Apple": isApple = true; isPineapple = false; isWatermelon = false; isStrawberry = false; isDagger = false; isSword = false; isMagicSword = false; isSpeedPotion = false; isDamagePotion = false; isInvinciblePotion = false; isRevivePotion = false; isLaserSword = false; isIceSword = false; isFlameSword = false; break;
                    case "Pineapple": isPineapple = true; isApple = false; isWatermelon = false; isStrawberry = false; isDagger = false; isSword = false; isMagicSword = false; isSpeedPotion = false; isDamagePotion = false; isInvinciblePotion = false; isRevivePotion = false; isLaserSword = false; isIceSword = false; isFlameSword = false; break;
                    case "Watermelon": isWatermelon = true; isApple = false; isPineapple = false; isStrawberry = false; isDagger = false; isSword = false; isMagicSword = false; isSpeedPotion = false; isDamagePotion = false; isInvinciblePotion = false; isRevivePotion = false; isLaserSword = false; isIceSword = false; isFlameSword = false; break;
                    case "Strawberry": isStrawberry = true; isApple = false; isPineapple = false; isWatermelon = false; isDagger = false; isSword = false; isMagicSword = false; isSpeedPotion = false; isDamagePotion = false; isInvinciblePotion = false; isRevivePotion = false; isLaserSword = false; isIceSword = false; isFlameSword = false; break;
                    case "Sword": isSword = true; isApple = false; isPineapple = false; isWatermelon = false; isStrawberry = false; isDagger = false; isMagicSword = false; isSpeedPotion = false; isDamagePotion = false; isInvinciblePotion = false; isRevivePotion = false; isLaserSword = false; isIceSword = false; isFlameSword = false; break;
                    case "Magic Sword": isMagicSword = true; isApple = false; isPineapple = false; isWatermelon = false; isStrawberry = false; isDagger = false; isSword = false; isSpeedPotion = false; isDamagePotion = false; isInvinciblePotion = false; isRevivePotion = false; isLaserSword = false; isIceSword = false; isFlameSword = false; break;
                    case "Speed Potion": isSpeedPotion = true; isApple = false; isPineapple = false; isWatermelon = false; isStrawberry = false; isDagger = false; isSword = false; isMagicSword = false; isDamagePotion = false; isInvinciblePotion = false; isRevivePotion = false; isLaserSword = false; isIceSword = false; isFlameSword = false; break;
                    case "Damage Potion": isDamagePotion = true; isApple = false; isPineapple = false; isWatermelon = false; isStrawberry = false; isDagger = false; isSword = false; isMagicSword = false; isSpeedPotion = false; isInvinciblePotion = false; isRevivePotion = false; isLaserSword = false; isIceSword = false; isFlameSword = false; break;
                    case "Invincible Potion": isInvinciblePotion = true; isApple = false; isPineapple = false; isWatermelon = false; isStrawberry = false; isDagger = false; isSword = false; isMagicSword = false; isSpeedPotion = false; isDamagePotion = false; isRevivePotion = false; isLaserSword = false; isIceSword = false; isFlameSword = false; break;
                    case "Revive Potion": isRevivePotion = true; isApple = false; isPineapple = false; isWatermelon = false; isStrawberry = false; isDagger = false; isSword = false; isMagicSword = false; isSpeedPotion = false; isDamagePotion = false; isInvinciblePotion = false; isLaserSword = false; isIceSword = false; isFlameSword = false; break;
                    case "Laser Sword": isLaserSword = true; isApple = false; isPineapple = false; isWatermelon = false; isStrawberry = false; isDagger = false; isSword = false; isMagicSword = false; isSpeedPotion = false; isDamagePotion = false; isInvinciblePotion = false; isRevivePotion = false; isIceSword = false; isFlameSword = false; break;
                    case "Ice Sword": isIceSword = true; isApple = false; isPineapple = false; isWatermelon = false; isStrawberry = false; isDagger = false; isSword = false; isMagicSword = false; isSpeedPotion = false; isDamagePotion = false; isInvinciblePotion = false; isRevivePotion = false; isLaserSword = false; isFlameSword = false; break;
                    case "Flame Sword": isFlameSword = true; isApple = false; isPineapple = false; isWatermelon = false; isStrawberry = false; isDagger = false; isSword = false; isMagicSword = false; isSpeedPotion = false; isDamagePotion = false; isInvinciblePotion = false; isRevivePotion = false; isLaserSword = false; isIceSword = false; break;
                }
            }
        }
    }

    private void UseActiveItem()
    {
        var itemAttributes = GameObject.Find("HeroCanvas/PlayerInventory").GetComponent<ItemAttributes>();
        
        if(isApple)
        {
            itemAttributes.UseItem();
            Debug.Log("Apple Worked");
            isApple = false;
            this.transform.GetChild(activeSlotIndexNum).GetChild(1).gameObject.SetActive(false);
            InventorySlots[activeSlotIndexNum - 1] = "0";
        }
        if(isPineapple)
        {
            itemAttributes.UseItem();
            Debug.Log("Pineapple Worked");
            isPineapple = false;
            this.transform.GetChild(activeSlotIndexNum).GetChild(2).gameObject.SetActive(false);
            InventorySlots[activeSlotIndexNum - 1] = "0";
        }
        if(isWatermelon)
        {
            itemAttributes.UseItem();
            Debug.Log("Watermelon Worked");
            isWatermelon = false;
            this.transform.GetChild(activeSlotIndexNum).GetChild(3).gameObject.SetActive(false);
            InventorySlots[activeSlotIndexNum - 1] = "0";
        }
        if(isStrawberry)
        {
            itemAttributes.UseItem();
            Debug.Log("Strawberry Worked");
            isStrawberry = false;
            this.transform.GetChild(activeSlotIndexNum).GetChild(4).gameObject.SetActive(false);
            InventorySlots[activeSlotIndexNum - 1] = "0";
        }
        if(isDagger)
        {
            itemAttributes.UseItem();
        }
        if(isSword)
        {
            itemAttributes.UseItem();
            Debug.Log("Sword Worked");
        }
        if(isMagicSword)
        {
            itemAttributes.UseItem();
            Debug.Log("Magic Sword Worked");
        }
        if(isSpeedPotion)
        {
            itemAttributes.UseItem();
            Debug.Log("Speed Potion Worked");
            isSpeedPotion = false;
            this.transform.GetChild(activeSlotIndexNum).GetChild(7).gameObject.SetActive(false);
            InventorySlots[activeSlotIndexNum - 1] = "0";
        }
        if(isDamagePotion)
        {
            itemAttributes.UseItem();
            Debug.Log("Damage Potion Worked");
            isDamagePotion = false;
            this.transform.GetChild(activeSlotIndexNum).GetChild(8).gameObject.SetActive(false);
            InventorySlots[activeSlotIndexNum - 1] = "0";
        }
        if(isInvinciblePotion)
        {
            itemAttributes.UseItem();
            Debug.Log("Invincible Potion Worked");
            isInvinciblePotion = false;
            this.transform.GetChild(activeSlotIndexNum).GetChild(9).gameObject.SetActive(false);
            InventorySlots[activeSlotIndexNum - 1] = "0";
        }
        if(isRevivePotion)
        {
            itemAttributes.UseItem();
            Debug.Log("Revive Potion Worked");
            isRevivePotion = false;
            this.transform.GetChild(activeSlotIndexNum).GetChild(10).gameObject.SetActive(false);
            InventorySlots[activeSlotIndexNum - 1] = "0";
        }
        if(isLaserSword)
        {
            itemAttributes.UseItem();
            Debug.Log("Laser Sword Worked");
        }
        if(isIceSword)
        {
            itemAttributes.UseItem();
            Debug.Log("ice Sword Worked");
        }
        if(isFlameSword)
        {
            itemAttributes.UseItem();
            Debug.Log("Flame Sword Worked");
        }

        ChangeActiveItem();
    }

    public void ToggleActiveSlot(int numValue)
    {
        ToggleActiveHighLight(numValue - 1);
    }

    public void ToggleActiveHighLight(int indexNum)
    {
        activeSlotIndexNum = indexNum;

        foreach(Transform inventorySlot in this.transform)
        {
            inventorySlot.GetChild(0).gameObject.SetActive(false);
        }

        this.transform.GetChild(indexNum).GetChild(0).gameObject.SetActive(true);

        ChangeActiveItem();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {   
            if(shopKeeper != null)
            {
                if(!shopKeeper.playerInRange)
                {
                    UseActiveItem();
                }
            }
        }

        //if(shopKeeper != null)
        //{
        //    if(shopKeeper.playerInRange)
        //    {
        //        Slot1.onClick.AddListener(() => { ToggleActiveSlot(1); ShowSellConfirmationPanel(); });
        //        Slot2.onClick.AddListener(() => { ToggleActiveSlot(2); ShowSellConfirmationPanel(); });
        //        Slot3.onClick.AddListener(() => { ToggleActiveSlot(3); ShowSellConfirmationPanel(); });
        //        Slot4.onClick.AddListener(() => { ToggleActiveSlot(4); ShowSellConfirmationPanel(); });
        //        Slot5.onClick.AddListener(() => { ToggleActiveSlot(5); ShowSellConfirmationPanel(); });
        //        Slot6.onClick.AddListener(() => { ToggleActiveSlot(6); ShowSellConfirmationPanel(); });
        //        Slot7.onClick.AddListener(() => { ToggleActiveSlot(7); ShowSellConfirmationPanel(); });
        //    }
        //    else
        //    {
        //        Slot1.onClick.RemoveAllListeners();
        //        Slot2.onClick.RemoveAllListeners();
        //        Slot3.onClick.RemoveAllListeners();
        //        Slot4.onClick.RemoveAllListeners();
        //        Slot5.onClick.RemoveAllListeners();
        //        Slot6.onClick.RemoveAllListeners();
        //        Slot7.onClick.RemoveAllListeners();
        //    }
        //}
    }
}
