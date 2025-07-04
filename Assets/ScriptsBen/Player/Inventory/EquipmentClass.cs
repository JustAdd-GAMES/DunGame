using UnityEngine;
using System.Collections.Generic;
[CreateAssetMenu(fileName = "NewEquipment", menuName = "Items/Equipment")]
public class EquipmentClass : ItemClass
{
    [Header("Equipment")]
    public EquipmentType equipmentType; 
    public enum EquipmentType
    {
        Accessory, 
        
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
