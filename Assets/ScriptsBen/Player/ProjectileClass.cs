using UnityEngine;
using System.Collections.Generic;

public class Projectile : MonoBehaviour
{
    [Header("Projectile Settings")]
    [SerializeField] private float speed = 6f;
    [SerializeField] private int damage = 5;
    [SerializeField] private float lifetime = 3f;
    [SerializeField] private LayerMask collisionLayers;
    
    private Animator animator;
    private Vector2 direction;
    private Rigidbody2D rb;

    // Add this field to Projectile
    public List<ItemEffectBehaviour> effectsToApply;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // When initializing the projectile, pass the effects
    public void Initialize(Vector2 fireDirection, List<ItemEffectBehaviour> effects = null)
    {
        direction = fireDirection.normalized; // Normalize the direction
        effectsToApply = effects;

        if (rb != null)
        {
            rb.linearVelocity = direction * speed; // Set the velocity of the Rigidbody2D
        }

        Destroy(gameObject, lifetime); // Destroy the projectile after its lifetime
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

        if (animator != null)
        {

            rb.linearVelocity = direction * speed * 0; // Stop the projectile's movement
            animator?.SetTrigger("isDie");
            //  disable the collider to prevent further collisions
            Collider2D collider = GetComponent<Collider2D>();
            if (collider != null)
            {
                collider.enabled = false;
            }

            if (rb != null)
            {
                rb.bodyType = RigidbodyType2D.Static; // Set Rigidbody to static to stop physics interactions
            }

            // Wait for the animation to finish before destroying the projectile
                Destroy(gameObject, animator.GetCurrentAnimatorStateInfo(0).length);

        }
        else
        {
            Destroy(gameObject); // Destroy the projectile immediately if no animator is present
        }
        
        
       
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