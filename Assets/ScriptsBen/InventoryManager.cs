using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    [Header("Inventory Manager")]

    [SerializeField] private GameObject slotHolder;
    [SerializeField] private GameObject[] slots; // Reference to the inventory UI GameObject

    public SlotClass[] slotScripts; // Array to hold SlotClass components

    [SerializeField] private ItemClass itemToAdd; // Item to add to the inventory at start
    [SerializeField] private ItemClass ItemtoRemove; // Item to remove from the inventory

    public ItemClass[] inventoryItems; // List of items in the inventory
    public ItemClass selectedItem; // Currently selected item in the inventory
    private SlotClass selectedSlot; // Currently selected slot in the inventory
    private SlotClass tempSlot; // The closest item to the mouse cursor
    private SlotClass newSlot; // The closest item to the mouse cursor
    bool isMovingItem; // Flag to check if an item is being moved
    [SerializeField] private Canvas canvas;
    [SerializeField] private GameObject itemCursor; // The item cursor prefab to show the item being moved

    private ItemClass tempItem; // Holds the item being moved

    [SerializeField] private Color slotHighlightColor = Color.blue;
    [SerializeField] private Color slotNormalColor = new Color(1, 1, 1, 0); // RGBA: white, alpha 0 (fully transparent)

    [SerializeField] private GameObject pickupPrefab; // Assign your pickup prefab in the Inspector

    public void Start()
    {
        slots = new GameObject[slotHolder.transform.childCount];
        slotScripts = new SlotClass[slotHolder.transform.childCount];

        for (int i = 0; i < slotHolder.transform.childCount; i++)
        {
            slots[i] = slotHolder.transform.GetChild(i).gameObject;
            slotScripts[i] = slots[i].GetComponent<SlotClass>();
            slotScripts[i].slotIndex = i; // Set the slot index
        }

        AddItem(itemToAdd); // Add an item to the inventory at start
        RefreshUI();

    }

    public void Update()
    {
        
           
       

        if (Input.GetMouseButtonDown(0))
            {
                if (isMovingItem)
                {
                    EndMoveItem();
                }
                else
                {
                    BeginMoveItem();
                }
            }
    }

    #region Inventory Utilities

    public void RefreshUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            // Keep inventoryItems in sync with slotScripts
            if (inventoryItems != null && i < inventoryItems.Length)
                inventoryItems[i] = slotScripts[i].itemInSlot;

            try
            {
                var item = slotScripts[i].itemInSlot;
                var itemImg = slots[i].transform.GetChild(0).GetComponent<Image>();
                itemImg.enabled = item != null;
                itemImg.sprite = item != null ? item.itemIcon : null;
                itemImg.color = Color.white; // Always white, so the icon is not tinted
            }
            catch
            {
                slots[i].transform.GetChild(0).GetComponent<Image>().sprite = null;
                slots[i].transform.GetChild(0).GetComponent<Image>().enabled = false;
            }
        }
    }


    public void AddItem(ItemClass itemToAdd)
    {
        for (int i = 0; i < slotScripts.Length; i++)
        {
            if (slotScripts[i].itemInSlot == null)
            {
                slotScripts[i].SetItem(itemToAdd);
                break;
            }
        }

        RefreshUI();
        Debug.Log($"Added {itemToAdd.itemName} to inventory.");
    }


    public void RemoveItem(ItemClass itemToRemove)
    {
        for (int i = 0; i < slotScripts.Length; i++)
        {
            if (slotScripts[i].itemInSlot == itemToRemove)
            {
                slotScripts[i].ClearSlot();
                break;
            }
        }

        RefreshUI();
        Debug.Log($"Removed {itemToRemove.itemName} from inventory.");
    }


    public bool ContainsItem(ItemClass item)
    {
        for (int i = 0; i < slotScripts.Length; i++)
        {
            if (slotScripts[i].itemInSlot == item)
            {
                return true;
            }
        }
        return false;
    }

    public void SelectItem(int index)
    {
        if (index >= 0 && index < slotScripts.Length)
        {
            selectedItem = slotScripts[index].itemInSlot;
        }
    }


    #endregion Inventory Utilities

    #region moving stuff around


    private SlotClass GetClosestSlot()
    {
        Vector2 mousePos = Input.mousePosition;
        float minDist = float.MaxValue;
        SlotClass closest = null;

        for (int i = 0; i < slots.Length; i++)
        {
            Vector2 slotScreenPos = RectTransformUtility.WorldToScreenPoint(
                canvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : canvas.worldCamera,
                slots[i].transform.position
            );
            float dist = Vector2.Distance(slotScreenPos, mousePos);
            Debug.Log($"Slot {i} screenPos: {slotScreenPos}, mousePos: {mousePos}, dist: {dist}");
            if (dist < 50f && dist < minDist)
            {
                minDist = dist;
                closest = slotScripts[i];
            }
        }
        return closest;
    }


    public bool BeginMoveItem()
    {
        selectedSlot = GetClosestSlot();
        if (selectedSlot == null || selectedSlot.itemInSlot == null)
        {
            return false; // no item to move
        }
        tempItem = selectedSlot.itemInSlot; // Store the item being moved
        selectedSlot.ClearSlot();
        isMovingItem = true;

        RefreshUI(); // Call this first to reset all colors

        // Highlight the item image in the selected slot AFTER RefreshUI
        var slotObj = slots[selectedSlot.slotIndex];
        var itemImg = slotObj.transform.GetChild(0).GetComponent<Image>();
        if (itemImg != null)
            itemImg.color = slotHighlightColor; // Set to blue

        // Highlight the slot background (grid)
        var slotBgImg = slots[selectedSlot.slotIndex].GetComponent<Image>();
        if (slotBgImg != null)
            slotBgImg.color = slotNormalColor; // Transparent for normal

        return true;
    }
    
    private bool EndMoveItem()
    {
        if (tempItem == null)
        {
            isMovingItem = false;
            // Reset color if needed
            if (selectedSlot != null)
            {
                var slotObj = slots[selectedSlot.slotIndex];
                var itemImg = slotObj.transform.GetChild(0).GetComponent<Image>();
                if (itemImg != null)
                    itemImg.color = slotNormalColor;
            }
            return false;
        }

        newSlot = GetClosestSlot();
        if (newSlot != null)
        {
            // Swap items if the new slot is not empty
            ItemClass itemInNewSlot = newSlot.itemInSlot;
            newSlot.SetItem(tempItem); // Place the moving item in the new slot

            if (itemInNewSlot != null)
            {
                selectedSlot.SetItem(itemInNewSlot); // Put the previous item back in the original slot
            }
            else
            {
                selectedSlot.ClearSlot(); // Original slot stays empty if nothing to swap
            }

            // Reset color
            var slotObj = slots[selectedSlot.slotIndex];
            var itemImg = slotObj.transform.GetChild(0).GetComponent<Image>();
            if (itemImg != null)
                itemImg.color = slotNormalColor;

            tempItem = null;
            isMovingItem = false;
            RefreshUI();
            return true;
        }
        else
        {
            // No valid slot found, drop item into the world as a pickup
            Vector3 spawnPosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 5f)); // 5f is distance from camera
            // When dropping an item into the world
            GameObject pickup = Instantiate(pickupPrefab, spawnPosition, Quaternion.identity);
            var pickupScript = pickup.GetComponent<PickUpScript>();
            if (pickupScript != null)
                pickupScript.SetItem(tempItem); // This sets both the item and the sprite

            // Reset color
            var slotObj = slots[selectedSlot.slotIndex];
            var itemImg = slotObj.transform.GetChild(0).GetComponent<Image>();
            if (itemImg != null)
                itemImg.color = slotNormalColor;

            tempItem = null;
            isMovingItem = false;
            RefreshUI();
            return false;
        }
    }

    #endregion moving stuff around

    public bool IsMovingItem => isMovingItem;
}
