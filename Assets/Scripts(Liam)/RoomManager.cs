using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    public static RoomManager Instance;

    public GameObject roomPrefab;
    public GridManager gridManager;

    private List<Cell> enteredCellsThisRound = new List<Cell>();

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            foreach (var cell in enteredCellsThisRound)
            {
                SpawnTwoAdjacentRooms(cell);
            }

            enteredCellsThisRound.Clear();
        }
    }

    public void NotifyRoomEntered(Cell cell)
    {
        if (!enteredCellsThisRound.Contains(cell))
        {
            enteredCellsThisRound.Add(cell);
            Debug.Log("Room entered at " + cell.gridPosition);
        }
    }

    void SpawnTwoAdjacentRooms(Cell originCell)
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

            Instantiate(roomPrefab, target.transform.position, Quaternion.identity);
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
}

