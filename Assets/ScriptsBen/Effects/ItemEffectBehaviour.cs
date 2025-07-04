
using UnityEngine;
public abstract class ItemEffectBehaviour : MonoBehaviour
{
    public virtual bool ShouldApplyOnAttack() => false;
    public virtual void OnAttack(Projectile projectile) { }

    public virtual bool ShouldApplyOnProjectileHit() => false;
    public virtual void OnProjectileHit(Projectile projectile, Collider2D collision) { }

    public virtual void OnAddToInventory(GameObject player) { }
    public virtual void OnRemoveFromInventory(GameObject player) { }
}