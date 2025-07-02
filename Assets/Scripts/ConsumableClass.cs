using UnityEngine;
[CreateAssetMenu(fileName = "NewConsumable", menuName = "Items/Consumable")]
public class ConsumableClass : ItemClass
{
    [Header("Consumable")]
    public float consumptionValue; // Value consumed when used
    public float consumptionDuration; // Duration of the effect
    public enum ConsumableType
    {
        Health, // Restores health
        Buff, // Provides a temporary buff
    }
    public ConsumableType consumableType; // Type of consumable
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
        return null;
    }
    public override ConsumableClass GetConsumable()
    {
        return this; // Tools are not consumables
    }
}
