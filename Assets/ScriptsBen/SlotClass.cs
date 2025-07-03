using UnityEngine;

public class SlotClass : MonoBehaviour
{
    public int slotIndex; // The index of this slot in the inventory
    public ItemClass itemInSlot; // The item currently in this slot

    // Default constructor
    public SlotClass() {}

    // Copy constructor
    public SlotClass(SlotClass other)
    {
        this.slotIndex = other.slotIndex;
        this.itemInSlot = other.itemInSlot;
    }

    public void SetItem(ItemClass item)
    {
        itemInSlot = item;
    }

    public void ClearSlot()
    {
        itemInSlot = null;
    }
}
