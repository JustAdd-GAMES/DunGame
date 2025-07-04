using UnityEngine;

public class MovementScript : MonoBehaviour
{
    [SerializeField] public float moveSpeed; // Serialized to show in Inspector
    private Rigidbody2D rb;
    private Vector2 movement;

    void Start()
    {
        moveSpeed = 3f; // Set default move speed
        rb = this.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
    }

    void FixedUpdate()
    {
        movement.Normalize();
        rb.linearVelocity = movement * moveSpeed; // Apply movement
    }

    // Updated method to increase speed using addition
    public void IncreaseSpeed(float amount)
    {
        moveSpeed += amount; // Add the speed increase
        Debug.Log($"New moveSpeed: {moveSpeed}");
    }

    // Optional: Method to decrease speed
    public void DecreaseSpeed(float amount)
    {
        moveSpeed -= amount; // Subtract the speed increase
        Debug.Log($"New moveSpeed: {moveSpeed}");
    }
}
