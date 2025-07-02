using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    [Header("Inventory Manager")]

    [SerializeField] private GameObject slotHolder;
    private GameObject[] slots;// Reference to the inventory UI GameObject

     
    public List<SlotClass> inventoryItems = new List<SlotClass>();
    [SerializeField] private ItemClass itemToAdd; // Item to add to the inventory at start

    [SerializeField] private ItemClass ItemtoRemove; // Item to remove from the inventory

    //
    public ItemClass selectedItem; // Currently selected item in the inventory

    //
    public void Start()
    {
        slots = new GameObject[slotHolder.transform.childCount]; // Initialize the slots array

        for (int i = 0; i < slotHolder.transform.childCount; i++)
        {
            slots[i] = slotHolder.transform.GetChild(i).gameObject; // Populate the slots array with child GameObjects
        }
        AddItem(itemToAdd); // Add the item to the inventory at start
        RefreshUI(); // Refresh the UI to show the added item
    }
    public void RefreshUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            try
            {
                slots[i].transform.GetChild(0).GetComponent<Image>().enabled = true;
                slots[i].transform.GetChild(0).GetComponent<Image>().sprite = inventoryItems[i].GetItem().itemIcon; // Clear the slot image
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

        //    inventoryItems.Add(item); // Add the item to the inventory list
    
        Debug.Log($"Added {item.itemName} to inventory.");
        SlotClass slots = ContainsItem(item); // Check if the item already exists in the inventory
        if (slots != null)
        {
            slots.AddQuantity(1); // If it exists, increase the quantity
        }
        else
        {
            SlotClass newSlot = new SlotClass(item, 1); // Create a new slot for the item
   
        }

        RefreshUI(); // Refresh the UI to show the added item
    }
    public bool RemoveItem(ItemClass item)
    {
        //  inventoryItems.Remove(item);
        SlotClass temp = ContainsItem(item); // Check if the item exists in the inventory
        if (temp != null)
        {
            if (temp.GetQuantity() > 1) // Ensure quantity doesn't go negative
                temp.SubQuantity(1); // If it exists, increase the quantity
            else
            {
                SlotClass slotToRemove = new SlotClass(); // Check if the item exists in the inventory

                foreach (SlotClass slot in inventoryItems)
                {

                    if (slot.GetItem() == item)
                    {
                        slotToRemove = slot; // Remove the slot containing the item
                        break; // Exit the loop after removing the item
                    }
                }
                inventoryItems.Remove(slotToRemove); // Remove the item from the inventory list
            }
        }
        else
        {
            return false;
        }
        RefreshUI(); // Refresh the UI to show the removed item
        return true; 
        
    }
    public SlotClass ContainsItem(ItemClass item)
    {
        foreach (SlotClass slot in inventoryItems)
        {
            if (slot.GetItem() == item)
            {
                return slot; // Return the slot containing the item
            }
        }
        return null; // Return null if the item is not found
    }
    
}
