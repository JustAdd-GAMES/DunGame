using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int currentEnemies = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void RegisterEnemy() // Adds enemies to the game
    {
        currentEnemies++;
        Debug.Log($"Enemy spawned. Total enemies: {currentEnemies}");
    }

    public void UnregisterEnemy() // Removes an enemy from the game
    {
        currentEnemies = Mathf.Max(0, currentEnemies - 1);
        Debug.Log($"Enemy Died. Total enemies: {currentEnemies}");
    }
}
