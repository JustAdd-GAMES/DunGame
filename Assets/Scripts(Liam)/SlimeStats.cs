using UnityEngine;

public class SlimeStats : MonoBehaviour, IDamageable
{
    [SerializeField] private Enemy enemyData;

    private float currentHealth;

    private void Awake()
    {
        if (enemyData == null)
        {
            //Debug.LogError($"{gameObject.name} has no Enemy ScriptableObject assigned!");
            return;
        }

        currentHealth = enemyData.EnemyHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        //Debug.Log($"{enemyData.EnemyName} took {damage} damage. Remaining: {currentHealth}");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log($"{enemyData.EnemyName} has died.");

        if (GameManager.Instance != null)
        {
            GameManager.Instance.UnregisterEnemy();
        }
        else
        {
            //Debug.Log("GameManager instance not foudn");
        }

        Destroy(gameObject);
    }

    public float GetDamageAmount()
    {
        return enemyData.EnemyDamage;
    }
}

