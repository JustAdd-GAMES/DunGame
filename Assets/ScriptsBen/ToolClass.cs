using UnityEngine;


[CreateAssetMenu(fileName = "NewTool", menuName = "Items/Tool")]
public class ToolClass : ItemClass
{
    [Header("Tool")]
    public ToolType toolType; // Type of tool 
    public enum ToolType
    {
        Weapon,
        Utility,

        // Add more tool types as needed
    }
    public int damage; //

    public int atkSpeed; // Speed of the tool

    public override ItemClass GetItem()
    {
        return this;
    }
    public override ToolClass GetTool()
    {
        return this;
    }
    public override EquipmentClass GetEquipment()
    {
        return null; // Tools are not misc items
    }
    public override ConsumableClass GetConsumable()
    {
        return null; // Tools are not consumables
    }
}
