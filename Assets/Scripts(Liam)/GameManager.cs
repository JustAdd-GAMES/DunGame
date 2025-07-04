using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int currentEnemies = 0;
    public bool allEnemiesCleared = false;
    public bool roomsSpawnedThisWave = false;
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
        allEnemiesCleared = false;
        Debug.Log($"Enemy spawned. Total enemies: {currentEnemies}");
    }

    public void UnregisterEnemy() // Removes an enemy from the game
    {
        currentEnemies = Mathf.Max(0, currentEnemies - 1);
        Debug.Log($"Enemy Died. Total enemies: {currentEnemies}");
        

        if (currentEnemies == 0 && !roomsSpawnedThisWave)
        {
            allEnemiesCleared = true;
            Debug.Log("All enemies cleared");

            if (RoomManager.Instance != null)
            {
                RoomManager.Instance.SpawnNewRoomsFromActiveRooms();
            }
            else
            {
                Debug.LogWarning("RoomManager instance not found");
            }
        }
    }

    public void DestroyAllEnemies()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemy in enemies)
        {
            Destroy(enemy);
        }

        currentEnemies = 0;
        allEnemiesCleared = true;

        Debug.Log("Manually killed all enemies with spacebar");
    }
}
