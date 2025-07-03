using UnityEngine;
using System.Collections.Generic;

public class GridManager : MonoBehaviour
{
    public GameObject cellPrefab;
    public GameObject[] roomPrefab;
    public int gridRadius = 5;
    public float spacing = 20f;

    public Dictionary<Vector2Int, Cell> grid = new Dictionary<Vector2Int, Cell>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GenerateGrid();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void GenerateGrid()
    {
        for (int x = -gridRadius; x <= gridRadius; x++)
        {
            for (int y = -gridRadius; y <= gridRadius; y++)
            {
                Vector3 worldPosition = new Vector3(x * spacing, y * spacing, 0);
                GameObject cell = Instantiate(cellPrefab, worldPosition, Quaternion.identity, transform);
                cell.name = $"Cell ({x},{y})";

                Cell cellScript = cell.GetComponent<Cell>();
                if (cellScript != null)
                {
                    cellScript.gridPosition = new Vector2Int(x, y);
                    grid[new Vector2Int(x, y)] = cellScript;
                }
            }
        }

        SpawnStartingRoom();
    }

    void SpawnStartingRoom()
    {
        foreach (Transform child in transform)
        {
            Cell cell = child.GetComponent<Cell>();
            if (cell != null && cell.gridPosition == Vector2Int.zero)
            {
                Instantiate(roomPrefab[0], cell.transform.position, Quaternion.identity);
                cell.hasRoom = true;
                cell.SpawnWalls(grid);
                break;
            }

        }
    }
}
