using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int currentEnemies = 0;
    public bool allEnemiesCleared = false;
    public bool roomsSpawnedThisWave = false;

    [SerializeField] private GameObject player;
    [SerializeField] private float roomSize = 20f;

    [Header("Item Drop Setttings")]
    [SerializeField] private GameObject[] itemPrefabs;
    [SerializeField] private float dropChance = 0.25f;
    private int roomsClearedWithoutDrop = 0;
    [SerializeField] private const int maxRoomsWithoutDrop = 4;

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
            roomsSpawnedThisWave = true;
            Debug.Log("All enemies cleared");

            Vector3 roomCenter = GetCurrentRoomCenter();
            TryDropItem(roomCenter);

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

    private void TryDropItem(Vector3 roomCenter)
    {
        int activeRoomCount = RoomManager.Instance != null ? RoomManager.Instance.GetEnteredRoomCount() : 0;
        float bonus = dropChance * 0.10f * activeRoomCount;
        float finalChance = dropChance + bonus;

        bool shouldDrop = Random.value < finalChance || roomsClearedWithoutDrop >= maxRoomsWithoutDrop;

        if (shouldDrop && itemPrefabs.Length > 0)
        {
            int index = Random.Range(0, itemPrefabs.Length);
            Instantiate(itemPrefabs[index], roomCenter, Quaternion.identity);
            roomsClearedWithoutDrop = 0;
            Debug.Log($"Item dropped: {itemPrefabs[index].name}");
        }
        else
        {
            roomsClearedWithoutDrop++;
            Debug.Log("Item didnt spawn");
        }
    }

    public Vector3 GetCurrentRoomCenter()
    {
        if (player == null)
        {
            Debug.LogWarning("Player not assigned in game manager");
            return Vector3.zero;
        }

        Vector3 pos = player.transform.position;

        float centerX = Mathf.Floor(pos.x / roomSize) * roomSize + roomSize / 2f;
        float centerY = Mathf.Floor(pos.y / roomSize) * roomSize + roomSize / 2f;

        return new Vector3(centerX, centerY, 0f);

    }
}
