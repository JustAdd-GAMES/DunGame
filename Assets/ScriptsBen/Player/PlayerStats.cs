using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public int maxPotions = 5;
    public int currentPotions;

    private Health health;

    private void Awake()
    {
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
}
// This class manages the player's stats, including health and potions.
// It allows taking damage, healing, and retrieving current and maximum health values.
