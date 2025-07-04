using UnityEngine;
using UnityEngine.SceneManagement; // Required for restarting the scene

public class Health : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100f;
    private float currentHealth;
    private float previousHealth; // To track health changes

    public float MaxHealth => maxHealth;
    public float CurrentHealth => currentHealth;

    private void Awake()
    {
        currentHealth = maxHealth;
        previousHealth = maxHealth; // Initialize previous health
    }

    private void Update()
    {
        // Check if the space bar is pressed
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ModifyHealth(-50f); // Take 50 damage
        }

        // Check if health has decreased
        if (currentHealth < previousHealth)
        {
            Debug.Log($"Player has lost health! Current Health: {currentHealth}");
            previousHealth = currentHealth; // Update previous health
        }
    }

    public void ModifyHealth(float amount)
    {
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);

        // Check if health is zero
        if (currentHealth <= 0)
        {
            OnHealthDepleted();
        }
    }

    public void SetHealth(float value)
    {
        currentHealth = Mathf.Clamp(value, 0, maxHealth);

        // Check if health is zero
        if (currentHealth <= 0)
        {
            OnHealthDepleted();
        }
    }

    private void OnHealthDepleted()
    {
        Debug.Log("Player health is zero. Restarting game...");
        RestartGame();
    }

    private void RestartGame()
    {
        // Reload the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
