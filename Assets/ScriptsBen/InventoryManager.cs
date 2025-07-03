using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    [Header("Inventory Manager")]

    [SerializeField] private GameObject slotHolder;
    [SerializeField] private GameObject[] slots;// Reference to the inventory UI GameObject
    [SerializeField] private ItemClass itemToAdd; // Item to add to the inventory at start
    [SerializeField] private ItemClass ItemtoRemove; // Item to remove from the inventory

    public ItemClass[] inventoryItems; // List of items in the inventory
    
    public ItemClass selectedItem; // Currently selected item in the inventory

    public void Start()
    {
        inventoryItems = new ItemClass[inventoryItems.Length]; // Initialize the inventory items array based on the number of slots
        slots = new GameObject[slotHolder.transform.childCount]; // Initialize the slots array

        for (int i = 0; i < slotHolder.transform.childCount; i++)
        {
            slots[i] = slotHolder.transform.GetChild(i).gameObject; // Populate the slots array with child GameObjects
        }

        RefreshUI(); // Refresh the UI to show the added item

        AddItem(itemToAdd); // Add the item to the inventory at start
        RemoveItem(ItemtoRemove); // Remove the item from the inventory at start

    }
    public void RefreshUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            try
            {
                slots[i].transform.GetChild(0).GetComponent<Image>().enabled = true;
                slots[i].transform.GetChild(0).GetComponent<Image>().sprite = inventoryItems[i].itemIcon; // Set the sprite of the image component to the item's icon

            }
            catch
            {
                slots[i].transform.GetChild(0).GetComponent<Image>().sprite = null; // If no item, set to null
                slots[i].transform.GetChild(0).GetComponent<Image>().enabled = false; // Disable the image component if no item is present
            }
        }
    }


    public void AddItem(ItemClass item)
    {

        inventoryItems.Add(item);


        RefreshUI(); // Refresh the UI to show the added item
        Debug.Log($"Added {item.itemName} to inventory.");

    }


    public void RemoveItem(ItemClass item)
    {

        inventoryItems.Remove(item); // Remove the item from the inventory list


        RefreshUI(); // Refresh the UI to show the removed item
        Debug.Log($"Removed {item.itemName} from inventory.");

    }
    
    public bool ContainsItem(ItemClass item)
    {
        if (inventoryItems.Contains(item))
        {
            Debug.Log($"Inventory contains {item.itemName}.");
            return true;
        }
        else
        {
            Debug.Log($"Inventory does not contain {item.itemName}.");
            return false;
        }
    }

    
}
