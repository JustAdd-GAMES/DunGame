using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Header("Projectile Settings")]
    [SerializeField] private float speed = 10f;
    [SerializeField] private int damage = 1;
    [SerializeField] private float lifetime = 3f;
    [SerializeField] private LayerMask collisionLayers;
    
    private Vector2 direction;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Initialize(Vector2 fireDirection)
    {
        direction = fireDirection.normalized;
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
        // Check if collision is in specified layers
        if ((collisionLayers.value & (1 << collision.gameObject.layer)) > 0)
        {
            HandleCollision(collision);
        }
    }

    private void HandleCollision(Collider2D collision)
    {
        // Damage logic
        IDamageable damageable = collision.GetComponent<IDamageable>();
        if (damageable != null)
        {
            damageable.TakeDamage(damage);
        }
        
        // Add additional collision effects here (particles, sounds, etc.)
        
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