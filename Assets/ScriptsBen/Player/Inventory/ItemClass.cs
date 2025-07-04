using System.Collections;
using UnityEngine;

public abstract class ItemClass : ScriptableObject
{
    [Header("Item")]// shared across all item types
    public int itemID; // Unique identifier for the item
    public string itemDescription; // Description of the item
    public string itemName;
    public Sprite itemIcon;
    public bool stackable = true;
   
   
   // public GameObject itemPrefab; // Prefab to instantiate when using the item
    public GameObject effectPrefab; // Assign a prefab with your ItemEffectBehaviour in the inspector
 
 
 
     public abstract ItemClass GetItem();
    public abstract ToolClass GetTool();
    public abstract EquipmentClass GetEquipment();
    public abstract ConsumableClass GetConsumable();

    public ItemEffect[] effects; // Assign in the Inspector

}
