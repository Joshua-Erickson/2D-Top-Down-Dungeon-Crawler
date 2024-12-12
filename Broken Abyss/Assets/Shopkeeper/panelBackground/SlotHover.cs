using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SlotHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Image slotImage;
    public Color hoverColor = Color.yellow; // Color when mouse hovers over the slot
    private Color defaultColor; // Default color of the slot

    void Start()
    {
        // Get the Image component from the slot
        slotImage = GetComponent<Image>();

        // Store the default color of the slot
        if (slotImage != null)
        {
            defaultColor = slotImage.color;
        }
    }

    // This is called when the mouse enters the slot
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (slotImage != null)
        {
            slotImage.color = hoverColor;
        }
    }

    // This is called when the mouse exits the slot
    public void OnPointerExit(PointerEventData eventData)
    {
        if (slotImage != null)
        {
            slotImage.color = defaultColor;
        }
    }
}
