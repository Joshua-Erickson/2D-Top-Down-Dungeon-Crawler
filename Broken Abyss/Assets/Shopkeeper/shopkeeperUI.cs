using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using TMPro;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class ShopkeeperUI : MonoBehaviour
{
    public GameObject MainOptionPanel; // Reference MainOptionPanel GameObject in Unity
    public GameObject panelBackground; //Reference InventoryPanel GameObject in Unity
    public Button entershopkeeperButton; // Reference Enter_Shop button in Unity
    public Button sellplayeritemsButton; // Reference Sell_PlayerItem button in Unity
    public GameObject Sell_ItemPanel; // Reference Sell_ItemPanel Gameobject in Unity
    public Button BackSellButton; // Reference Back_Button button in Unity
    public Button ExitSellButton; // Reference Exit_Button button in Unity
    public Button backButton; // Reference InventoryBack_Button button in Unity
    public Button exitButton; // Reference InventoryExit_Button button in Unity
    public GameObject ItemMenu; // Reference ItemOptionMenu
    public ShopItem selectedItem;
    public Button BuyItem_Button; // Reference BuyItem_Button button in Unity

    // Reference the inventory slot for SellConfirmationPanel items
    public GameObject SellConfirmationPanel_Item1; // Reference to Sell Confirmation Panel for Item 1
    public Button Yes_Button; // Yes button inside SellConfirmationPanel_Item1
    public Button No_Button;  // No button inside SellConfirmationPanel_Item1

    // References to the player's inventory
    public Inventory playerInventory;
    private GoldCounter goldCounter;

    public Button Sell_ItemConfirmButton;

    // References to Lore Generation stuff
    public GameObject LoreGeneration; // Reference LoreGeneration GameObject in Unity
    public TMP_Text LoreText; // Reference LoreText text inside LoreGeneration GameObject in Unity 
    public Button ItemLore_Button;

    public Button slot1Button;
    public Button slot2Button;
    public Button slot3Button;
    public Button slot4Button;
    public Button slot5Button;
    public Button slot6Button;
    public Button slot7Button;
    public Button slot8Button;
    public Button slot9Button;

    private Color defaultColor;
    public Color hoverColor = Color.yellow;

    // OpenAI API fields
    private static readonly string apiUrl = "https://api.openai.com/v1/chat/completions";
    private string apiKey = ""; // Replace with your actual OpenAI API key
    private HttpClient client;
    private bool apiFalse;

    void Start()
    {
        // Initialize HttpClient for OpenAI API requests
        client = new HttpClient();
        if(apiKey == "")
        {
            apiFalse = true;
        }
        else
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);
        }

        // Ensure all panels are hidden at the start
        MainOptionPanel.SetActive(false);
        panelBackground.SetActive(false);
        ItemMenu.SetActive(false);
        Sell_ItemPanel.SetActive(false);
        SellConfirmationPanel_Item1.SetActive(false);
        LoreGeneration.SetActive(false);

        // Get the gold counter component
        goldCounter = GameObject.Find("HeroCanvas/GoldCounter").GetComponent<GoldCounter>();

        // Add listeners for button clicks
        entershopkeeperButton.onClick.AddListener(EnterShopAction);
        sellplayeritemsButton.onClick.AddListener(SellItemAction);
        BackSellButton.onClick.AddListener(BackSellAction);
        ExitSellButton.onClick.AddListener(ExitSellAction);
        backButton.onClick.AddListener(BackAction);
        exitButton.onClick.AddListener(ExitAction);
        BuyItem_Button.onClick.AddListener(OnBuyItemClicked);

        // Add listener to Sell_ItemConfirmButton to open the confirmation panel
        Sell_ItemConfirmButton.onClick.AddListener(OpenSellConfirmationPanel);

        // Add listener for the ItemLore_Button
        ItemLore_Button.onClick.AddListener(OnItemLoreClicked);

        // Set up Yes and No button actions in the Sell Confirmation Panel
        Yes_Button.onClick.AddListener(ConfirmSell);
        No_Button.onClick.AddListener(CloseSellConfirmationPanel);

        // Store the original color of the buttons
        defaultColor = entershopkeeperButton.GetComponent<Image>().color;

        // Add EventTriggers for hover effects on Buy button
        AddEventTrigger(entershopkeeperButton, OnBuyButtonHoverEnter, EventTriggerType.PointerEnter);
        AddEventTrigger(entershopkeeperButton, OnBuyButtonHoverExit, EventTriggerType.PointerExit);

        // Add listeners for shopkeeper inventory slots with item names and prices
        slot1Button.onClick.AddListener(() => { OnSlotClicked("Sword", 25); playerInventory.ShowBuyPrice(); });
        slot2Button.onClick.AddListener(() => { OnSlotClicked("Magic Sword", 75); playerInventory.ShowBuyPrice(); });
        slot3Button.onClick.AddListener(() => { OnSlotClicked("Ice Sword", 100); playerInventory.ShowBuyPrice(); });
        slot4Button.onClick.AddListener(() => { OnSlotClicked("Flame Sword", 150); playerInventory.ShowBuyPrice(); });
        slot5Button.onClick.AddListener(() => { OnSlotClicked("Laser Sword", 200); playerInventory.ShowBuyPrice(); });
        slot6Button.onClick.AddListener(() => { OnSlotClicked("Speed Potion", 15); playerInventory.ShowBuyPrice(); });
        slot7Button.onClick.AddListener(() => { OnSlotClicked("Damage Potion", 15); playerInventory.ShowBuyPrice(); });
        slot8Button.onClick.AddListener(() => { OnSlotClicked("Invincible Potion", 50); playerInventory.ShowBuyPrice(); });
        slot9Button.onClick.AddListener(() => { OnSlotClicked("Revive Potion", 75); playerInventory.ShowBuyPrice(); });
    }

    // Show the LoreGeneration panel and display lore for the selected item
    private async void OnItemLoreClicked()
    {
        LoreGeneration.SetActive(true);

        if(apiFalse)
        {
            LoreText.text = "Needs Api Key.";
        }
        else
        {
            LoreText.text = "Generating lore..."; // Display a loading message

            if(selectedItem != null)
            {
                string lore = await GenerateLore(selectedItem.itemID);
                LoreText.text = lore;
            }
        }
    }

    // Method to generate lore using OpenAI API for a given item
    private async Task<string> GenerateLore(string itemName)
    {
        string prompt = $"Write a short 10-word fantasy description for an item called {itemName}.";
        return await CallOpenAIAPI(prompt);
    }

    // Function to call OpenAI ChatGPT API
    private async Task<string> CallOpenAIAPI(string prompt)
    {
        var payload = new
        {
            model = "gpt-3.5-turbo",
            messages = new[]
            {
                new { role = "system", content = "You are a creative fantasy lore writer." },
                new { role = "user", content = prompt }
            },
            max_tokens = 20
        };

        var jsonPayload = JsonConvert.SerializeObject(payload);
        var content = new StringContent(jsonPayload, System.Text.Encoding.UTF8, "application/json");

        HttpResponseMessage response = await client.PostAsync(apiUrl, content);

        if (response.IsSuccessStatusCode)
        {
            string resultJson = await response.Content.ReadAsStringAsync();
            JObject resultData = JObject.Parse(resultJson);

            string lore = resultData["choices"]?[0]?["message"]?["content"]?.ToString();
            return lore ?? "No lore was generated.";
        }
        else
        {
            return $"Error: {response.StatusCode} - {await response.Content.ReadAsStringAsync()}";
        }
    }

    void OnDestroy()
    {
        // Dispose of the HttpClient when the script is destroyed to free resources
        client?.Dispose();
    }

    private void OpenSellConfirmationPanel()
    {
        if (SellConfirmationPanel_Item1 != null)
        {
            SellConfirmationPanel_Item1.SetActive(true);
        }
    }


    private void ConfirmSell()
    {
        // Ensure activeItem is correctly set before selling
        Debug.Log($"Attempting to sell {playerInventory.activeItem}");

        // Get the price of the active item from the inventory
        int sellPrice = playerInventory.GetActiveItemPrice();

        // Add the item's price to the player's gold
        if (goldCounter != null)
        {
            goldCounter.AddGold(sellPrice);
            Debug.Log($"Added {sellPrice} gold for selling {playerInventory.activeItem}");
        }

        // Perform the item removal from the inventory
        playerInventory.RemoveItemInventory();

        // Close the Sell Confirmation Panel after selling
        CloseSellConfirmationPanel();
    }


    private void CloseSellConfirmationPanel()
    {
        if (SellConfirmationPanel_Item1 != null)
        {
            SellConfirmationPanel_Item1.SetActive(false);
        }
    }

    private void SellItemAction()
    {
        MainOptionPanel.SetActive(false);
        Sell_ItemPanel.SetActive(true);
    }

    void BackSellAction()
    {
        Sell_ItemPanel.SetActive(false);
        MainOptionPanel.SetActive(true);
    }

    void BackAction()
    {
        panelBackground.SetActive(false);
        ItemMenu.SetActive(false);
        MainOptionPanel.SetActive(true);
        LoreGeneration.SetActive(false);

    }

    void ExitAction()
    {
        panelBackground.SetActive(false);
        ItemMenu.SetActive(false);
        LoreGeneration.SetActive(false);
        if (playerInventory != null)
        {
            playerInventory.enabled = true;
        }
    }

    void ExitSellAction()
    {
        panelBackground.SetActive(false);
        ItemMenu.SetActive(false);
        Sell_ItemPanel.SetActive(false);
    }

    void OnBuyItemClicked()
    {
        if (selectedItem != null && playerInventory != null && goldCounter != null)
        {
            if (goldCounter.GetGold() >= selectedItem.price)
            {
                goldCounter.RemoveGold(selectedItem.price);
                if (playerInventory.CheckInventorySpace())
                {
                    playerInventory.AddItemInventory(selectedItem.itemID);
                    ItemMenu.SetActive(false);
                }
                else
                {
                    Debug.Log("No space in the player's inventory!");
                }
            }
            else
            {
                Debug.Log("Not enough gold to purchase this item.");
            }
        }
    }


    // This method is called for when the user click on a different shopkeeper inventory slot
    public void OnSlotClicked(string itemName, int price)
    {
        LoreGeneration.SetActive(false);
        selectedItem = new ShopItem(itemName, price);

        if (selectedItem != null)
        {
            Debug.Log($"Selected {selectedItem.itemID} from shopkeeper inventory, price: {selectedItem.price}");
            ItemMenu.SetActive(true);
        }
    }

    void EnterShopAction()
    {
        MainOptionPanel.SetActive(false);
        panelBackground.SetActive(true);
    }

    private void AddEventTrigger(Button button, System.Action<BaseEventData> action, EventTriggerType triggerType)
    {
        
        EventTrigger trigger = button.gameObject.GetComponent<EventTrigger>();
        if (trigger == null)
        {
            trigger = button.gameObject.AddComponent<EventTrigger>();
        }

        EventTrigger.Entry entry = new EventTrigger.Entry
        {
            eventID = triggerType
        };
        entry.callback.AddListener((eventData) => action(eventData));
        trigger.triggers.Add(entry);
    }

    void OnBuyButtonHoverEnter(BaseEventData eventData)
    {
        entershopkeeperButton.GetComponent<Image>().color = hoverColor;
    }

    void OnBuyButtonHoverExit(BaseEventData eventData)
    {
        entershopkeeperButton.GetComponent<Image>().color = defaultColor;
    }
}