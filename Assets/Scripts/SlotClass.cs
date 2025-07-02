using UnityEngine;

public class SlotClass
{
    [SerializeField] private ItemClass item; // The item in the slot
    [SerializeField] private int quantity; // Quantity of the item in the slot
    public SlotClass()
    {
        item = null;
        quantity = 0;

    }
    public SlotClass(ItemClass _item, int _quantity)
    {
        item = _item;

    }
    public ItemClass GetItem()
    {
        return item;
    }
    public int GetQuantity()
    {
        return quantity;
    }
    public void AddQuantity(int _quantity)
    {
        quantity += _quantity;
    }
    public void SubQuantity(int _quantity)
    {
        quantity -= _quantity;
        
    }

}
