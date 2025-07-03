using UnityEngine;

public class PickUpScript : MonoBehaviour
{
    [SerializeField] private ItemClass item; // Assign this in the Inspector
    private InventoryManager inventoryManager;

    private void Awake()
    {
        inventoryManager = FindFirstObjectByType<InventoryManager>();
        UpdateSprite();
    }

    public void SetItem(ItemClass newItem)
    {
        item = newItem;
        UpdateSprite();
    }

    private void UpdateSprite()
    {
        var sr = GetComponent<SpriteRenderer>();
        if (sr != null && item != null)
            sr.sprite = item.itemIcon;
        else if (sr != null)
            sr.sprite = null;
    }

    private void OnMouseDown()
    {
        if (inventoryManager == null || item == null)
            return;

        // Check if there is space in the inventory
        bool added = false;
        for (int i = 0; i < inventoryManager.slotScripts.Length; i++)
        {
            if (inventoryManager.slotScripts[i].itemInSlot == null)
            {
                inventoryManager.AddItem(item);
                added = true;
                break;
            }
        }

        if (added)
        {
            Destroy(gameObject); // Remove the pickup from the scene
        }
        // else: Optionally, give feedback that inventory is full
    }
}
