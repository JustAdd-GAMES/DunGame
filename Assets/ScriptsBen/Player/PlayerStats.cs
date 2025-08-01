using Unity.VisualScripting;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public int maxPotions = 5;
    public int currentPotions;

    public float potionCooldown = 30f;
    public float currnetCoolDownTime;// Cooldown time in seconds for potions

    private Health health;

    private void Awake()
    {
        currentPotions = maxPotions;
        health = GetComponent<Health>();
    }

    public void TakeDamage(DamageData damage)
    {
        // Trigger item effects, etc.
        GetComponent<InventoryManager>()?.TriggerDamageEffects(damage);

        // Apply damage via Health
        health.ModifyHealth(-damage.amount);
    }

    public void Heal(float amount)
    {
        health.ModifyHealth(amount);
    }

    public float GetCurrentHealth() => health.CurrentHealth;
    public float GetMaxHealth() => health.MaxHealth;

    public int GetCurrentPotions() => currentPotions;
    public int SetCurrentPotions(int value)
    {
        currentPotions = Mathf.Clamp(value, 0, maxPotions);
        return currentPotions;
    }

    public void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.RightControl) && currentPotions > 0)
        {
            // Use a potion
            currentPotions--;
            Heal(25f);

            Debug.Log($"Potion used! Remaining Potions: {currentPotions}");
        }
        else if (Input.GetKeyDown(KeyCode.H) && currentPotions <= 0)
        {
            // No potions left
            Debug.Log("No potions left to use!");
        }

        if (currentPotions < maxPotions)
        {
            // Increment cooldown time
            currnetCoolDownTime += Time.deltaTime;

            if (currnetCoolDownTime >= potionCooldown)
            {
                // Regenerate a potion
                currentPotions++;
                currnetCoolDownTime = 0f; // Reset cooldown time
                Debug.Log($"Potion regenerated! Current Potions: {currentPotions}");
            }
        }

    }

}
// This class manages the player's stats, including health and potions.
// It allows taking damage, healing, and retrieving current and maximum health values.
