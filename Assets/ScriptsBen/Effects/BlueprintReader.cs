using UnityEngine;
using UnityEngine.UI;

public class BlueprintReader : MonoBehaviour
{
    [Header("Assign the item asset here")]
    public ItemClass itemAsset;

    [Header("UI References")]
    public Image itemIconImage;
    public Text itemNameText;
    public Text itemDescriptionText;

    [Header("Optional: SpriteRenderer Source")]
    public SpriteRenderer spriteSource; // Assign this if you want to use a sprite from a SpriteRenderer

    private void Start()
    {
        Sprite iconToUse = null;

        if (spriteSource != null && spriteSource.sprite != null)
        {
            iconToUse = spriteSource.sprite;
        }
        else if (itemAsset != null && itemAsset.itemIcon != null)
        {
            iconToUse = itemAsset.itemIcon;
        }

        // Display item icon
        if (itemIconImage != null && iconToUse != null)
            itemIconImage.sprite = iconToUse;

        // Display item name
        if (itemNameText != null && itemAsset != null)
            itemNameText.text = itemAsset.itemName;

        // Display item description
        if (itemDescriptionText != null && itemAsset != null)
            itemDescriptionText.text = itemAsset.itemDescription;
    }
}