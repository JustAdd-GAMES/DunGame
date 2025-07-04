using UnityEngine;

public class PickUpScript : MonoBehaviour
{
    [SerializeField] private ItemClass item;
    private InventoryManager inventoryManager;

    private void Awake()
    {
        // Find the InventoryManager in the scene
        inventoryManager = Object.FindFirstObjectByType<InventoryManager>();

        // Render the sprite based on the item's icon
        var spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null && item != null && item.itemIcon != null)
        {
            spriteRenderer.sprite = item.itemIcon;
        }
    }

    private void OnMouseDown()
    {
        if (inventoryManager != null && item != null)
        {
            // Add the item to the inventory
            inventoryManager.AddItem(item);

            // Destroy the pickup object
            Destroy(gameObject);
        }
    }

    // Method to set the item data
    public void SetItem(ItemClass newItem)
    {
        item = newItem;

        // Update the sprite when the item is set
        var spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null && item != null && item.itemIcon != null)
        {
            spriteRenderer.sprite = item.itemIcon;
        }
    }
}
