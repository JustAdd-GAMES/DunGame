using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    public static RoomManager Instance;

    public GameObject roomPrefab;
    public GridManager gridManager;

    private List<Cell> enteredCellsThisRound = new List<Cell>(); // List of all Active Rooms

    private int playerDepth; // Depth of player decides the difficulty (how far from center)
    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) // On Space for each Active Room spawn 2 new roosm
        {
            if (GameManager.Instance.allEnemiesCleared)
            {
                foreach (var cell in enteredCellsThisRound)
                {
                    SpawnTwoAdjacentRooms(cell);
                }

                enteredCellsThisRound.Clear();
                GameManager.Instance.allEnemiesCleared = false;
            }
            else
            {
                // Manually kills all enemies for testing
                GameManager.Instance.DestroyAllEnemies();
                foreach (var cell in enteredCellsThisRound)
                {
                    SpawnTwoAdjacentRooms(cell);
                }
                enteredCellsThisRound.Clear();
                GameManager.Instance.allEnemiesCleared = false;
            }
        }
    }

    void SpawnTwoAdjacentRooms(Cell originCell) // Determines which cells are available for spawning and then spawns them
    {
        Vector2Int[] directions = new Vector2Int[]
        {
            Vector2Int.up, Vector2Int.right, Vector2Int.down, Vector2Int.left
        };

        List<Cell> spawnable = new List<Cell>();

        foreach (var dir in directions)
        {
            Vector2Int targetPos = originCell.gridPosition + dir;
            Cell neighbor = FindCellAt(targetPos);
            if (neighbor != null && !neighbor.hasRoom)
            {
                spawnable.Add(neighbor);
            }
        }

        int count = Mathf.Min(2, spawnable.Count);
        for (int i = 0; i < count; i++)
        {
            Cell target = spawnable[Random.Range(0, spawnable.Count)];
            spawnable.Remove(target);

            GameObject roomInstance = Instantiate(roomPrefab, target.transform.position, Quaternion.identity);
            target.roomObject = roomInstance;
            target.hasRoom = true;
            target.SpawnWalls(gridManager.grid);

            Vector2Int direction = target.gridPosition - originCell.gridPosition;
            originCell.RemoveWallFacing(direction);
            target.RemoveWallFacing(-direction);
        }
    }

    Cell FindCellAt(Vector2Int gridPos)
    {
        foreach (Transform child in gridManager.transform)
        {
            Cell cell = child.GetComponent<Cell>();
            if (cell != null && cell.gridPosition == gridPos)
                return cell;
        }
        return null;
    }

    public void NotifyRoomEntered(Cell cell) // Updates Active Room List
    {
        if (!enteredCellsThisRound.Contains(cell))
        {
            enteredCellsThisRound.Add(cell);
            Vector2Int pos = cell.gridPosition;
            playerDepth = Mathf.Max(Mathf.Abs(pos.x), Mathf.Abs(pos.y));
            Debug.Log($"Player at depth: {playerDepth}");

            GameObject room = cell.roomObject;
            if (room != null)
            {
                EnemySpawner[] spawners = room.GetComponentsInChildren<EnemySpawner>();
                foreach (var spawner in spawners)
                {
                    spawner.TriggerSpawn();
                }
            }
        }
    }
}

