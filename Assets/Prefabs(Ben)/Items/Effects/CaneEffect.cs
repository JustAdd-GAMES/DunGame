using UnityEngine;

[CreateAssetMenu(menuName = "ItemEffects/CaneEffect")]
public class CaneEffect : ItemEffect
{
    public float speedIncreaseAmount = 1.5f; // Amount to increase speed by

    public override void ApplyEffect(GameObject player)
    {
        var movement = player.GetComponent<MovementScript>();
        if (movement != null)
        {
            Debug.Log($"Applying CaneEffect: Increasing speed by {speedIncreaseAmount}");
            movement.IncreaseSpeed(speedIncreaseAmount); // Add the speed increase
        }
        else
        {
            Debug.LogWarning("MovementScript not found on player!");
        }
    }

    public override void RemoveEffect(GameObject player)
    {
        var movement = player.GetComponent<MovementScript>();
        if (movement != null)
        {
            Debug.Log($"Removing CaneEffect: Decreasing speed by {speedIncreaseAmount}");
            movement.DecreaseSpeed(speedIncreaseAmount); // Subtract the speed increase
        }
        else
        {
            Debug.LogWarning("MovementScript not found on player!");
        }
    }
}
