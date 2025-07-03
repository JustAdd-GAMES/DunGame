using UnityEngine;
using System.Collections.Generic;
[CreateAssetMenu(fileName = "NewMisc", menuName = "Items/Misc")]
public class EquipmentClass : ItemClass
{
    [Header("Equipment")]
    public EquipmentType equipmentType; // Type of equipment
    public enum EquipmentType
    {
        Accessory, // Rings, necklaces, etc.
        // Add more equipment types as needed
    }

    
    public override ItemClass GetItem()
    {
        return this;
    }
    public override ToolClass GetTool()
    {
        return null;
    }
    public override EquipmentClass GetEquipment()
    {
        return this; // Tools are not misc items
    }
    public override ConsumableClass GetConsumable()
    {
        return null; // Tools are not consumables
    }
}
