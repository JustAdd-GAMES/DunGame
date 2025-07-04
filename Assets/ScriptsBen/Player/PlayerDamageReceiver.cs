using UnityEngine;

[RequireComponent(typeof(PlayerStats))]
public class PlayerDamageReceiver : MonoBehaviour
{
    private PlayerStats stats;

    [SerializeField]
    private float damageTickInterval = 1f;
    private float damageCooldownTimer = 0f;

    void Awake()
    {
        stats = GetComponent<PlayerStats>();
    }

    private void Update()
    {
        if (damageCooldownTimer > 0)
        {
            damageCooldownTimer -= Time.deltaTime;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (!other.CompareTag("EnemyDamage")) return;

        if (damageCooldownTimer > 0) return;

        var slime = other.GetComponent<SlimeBehavior>() ?? other.GetComponentInParent<SlimeBehavior>();
        if (slime == null) return;

        float damage = slime.slimeData?.EnemyDamage ?? 0f;
        if (damage <= 0f) return;

        stats.TakeDamage(new DamageData(
            damage,
            slime.gameObject,
            gameObject,
            transform.position
        ));

        damageCooldownTimer = damageTickInterval;
    }
}
