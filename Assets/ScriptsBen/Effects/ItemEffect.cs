using UnityEngine;
[CreateAssetMenu(fileName = "NewEFffect", menuName = "Effects/ItemEffect")]
// BASE CLASS FOR ALL ITEM EFFECTS
public abstract class ItemEffect : ScriptableObject
{
    public string effectDescription;
    
    // Called when item is added to inventory
    public virtual void ApplyEffect(GameObject player) {}
    
    // Called when item is removed from inventory
    public virtual void RemoveEffect(GameObject player) {}
    
    // Called when player attacks
    public virtual void OnAttack(Projectile projectile) {}
    
    // Called when player takes damage
    public virtual void OnDamageTaken(DamageData damage) {}
}

