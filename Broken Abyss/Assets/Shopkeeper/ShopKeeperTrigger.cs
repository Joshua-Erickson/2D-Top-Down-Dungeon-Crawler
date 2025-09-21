using UnityEngine;

public class ShopkeeperTrigger : MonoBehaviour
{
    public GameObject MainOptionPanel;  // Drag your panel here in the inspector

    public bool playerInRange = false;  // To track if player is near shopkeeper

    public Inventory playerInventory;

    // Start is called before the first frame update
    void Start()
    {
        // Make the MainOptionPanel invisible at the start of the game
        MainOptionPanel.SetActive(false);

        playerInventory = GameObject.Find("HeroCanvas/PlayerInventory").GetComponent<Inventory>();
    }

    // Update is called once per frame
    void Update()
    {
        // If the player is in range and presses "E", toggle the panel visibility
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            // Toggle visibility of the panel
            MainOptionPanel.SetActive(!MainOptionPanel.activeSelf);
            Debug.Log("Test3");
        }

        if(playerInRange)
        {
            if(playerInventory.activeSlotIndexNum == 0)
            {
                playerInventory.ToggleActiveSlot(2);
            }
        }
    }

    // Detect when the player enters the shopkeeper's trigger collider
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInventory.Slot2.onClick.AddListener(() => { playerInventory.ToggleActiveSlot(2); playerInventory.ShowSellConfirmationPanel(); });
            playerInventory.Slot3.onClick.AddListener(() => { playerInventory.ToggleActiveSlot(3); playerInventory.ShowSellConfirmationPanel(); });
            playerInventory.Slot4.onClick.AddListener(() => { playerInventory.ToggleActiveSlot(4); playerInventory.ShowSellConfirmationPanel(); });
            playerInventory.Slot5.onClick.AddListener(() => { playerInventory.ToggleActiveSlot(5); playerInventory.ShowSellConfirmationPanel(); });
            playerInventory.Slot6.onClick.AddListener(() => { playerInventory.ToggleActiveSlot(6); playerInventory.ShowSellConfirmationPanel(); });
            playerInventory.Slot7.onClick.AddListener(() => { playerInventory.ToggleActiveSlot(7); playerInventory.ShowSellConfirmationPanel(); });

            playerInRange = true;  // Player is in range
            Debug.Log("Test");
        }
    }

    // Detect when the player leaves the shopkeeper's trigger collider
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInventory.Slot2.onClick.RemoveAllListeners();
            playerInventory.Slot3.onClick.RemoveAllListeners();
            playerInventory.Slot4.onClick.RemoveAllListeners();
            playerInventory.Slot5.onClick.RemoveAllListeners();
            playerInventory.Slot6.onClick.RemoveAllListeners();
            playerInventory.Slot7.onClick.RemoveAllListeners();

            playerInRange = false;  // Player is out of range
            MainOptionPanel.SetActive(false);  // Automatically close the panel when leaving range
            Debug.Log("Test2");
        }
    }
}


