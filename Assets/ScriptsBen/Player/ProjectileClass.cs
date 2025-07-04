using UnityEngine;
using System.Collections.Generic;

public class Projectile : MonoBehaviour
{
    [Header("Projectile Settings")]
    [SerializeField] private float speed = 6f;
    [SerializeField] private int damage = 5;
    [SerializeField] private float lifetime = 3f;
    [SerializeField] private LayerMask collisionLayers;
    
    private Vector2 direction;
    private Rigidbody2D rb;

    // Add this field to Projectile
    public List<ItemEffectBehaviour> effectsToApply;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // When initializing the projectile, pass the effects
    public void Initialize(Vector2 fireDirection, List<ItemEffectBehaviour> effects = null)
    {
        direction = fireDirection.normalized;
        effectsToApply = effects;
        Destroy(gameObject, lifetime);
    }

    private void FixedUpdate()
    {
        MoveProjectile();
    }

    private void MoveProjectile()
    {
        rb.linearVelocity = direction * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Ignore collisions with objects tagged as "Pickup"
        if (collision.CompareTag("Pickup"))
            return;

        // Check if collision is in specified layers
        if ((collisionLayers.value & (1 << collision.gameObject.layer)) > 0)
        {
            HandleCollision(collision);
        }
    }

    private void HandleCollision(Collider2D collision)
    {
        // Damage logic
        IDamageable damageable = collision.GetComponentInParent<IDamageable>();
        if (damageable != null)
        {
            damageable.TakeDamage(damage);
        }
        
        // Add additional collision effects here (particles, sounds, etc.)
        if (effectsToApply != null)
        {
            foreach (var effect in effectsToApply)
            {
                if (effect != null && effect.ShouldApplyOnProjectileHit())
                    effect.OnProjectileHit(this, collision);
            }
        }
        
        Destroy(gameObject);
    }

    // Visualize direction in editor
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + (Vector3)direction * 1f);
    }
}

// Damageable interface for objects that can take damage
public interface IDamageable
{
    void TakeDamage(int damageAmount);
}