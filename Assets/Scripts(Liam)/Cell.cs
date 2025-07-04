using UnityEngine;
using System.Collections.Generic;

public class Cell : MonoBehaviour
{
    public Vector2Int gridPosition;
    public bool hasRoom = false;
    public bool entered = false;
    public bool roomSpawned = false;

    public int depth;

    public GameObject topWall;
    public GameObject bottomWall;
    public GameObject leftWall;
    public GameObject rightWall;

    public GameObject horizontalWallPrefab;
    public GameObject veritcalWallPrefab;

    public GameObject roomObject;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && hasRoom && !entered)
        {
            entered = true;
            RoomManager.Instance.NotifyRoomEntered(this);
            //Debug.Log("Entered room at " + gridPosition);
        }
    }

    public void SpawnWalls(Dictionary<Vector2Int, Cell> grid)
    {
        Vector2Int up = gridPosition + Vector2Int.up;
        Vector2Int down = gridPosition + Vector2Int.down;
        Vector2Int left = gridPosition + Vector2Int.left;
        Vector2Int right = gridPosition + Vector2Int.right;

        if (!grid.ContainsKey(up) || !grid[up].hasRoom)
        {
            topWall = Instantiate(horizontalWallPrefab, transform.position + new Vector3(0, 10, 0), Quaternion.identity, transform);
        }

        if (!grid.ContainsKey(down) || !grid[down].hasRoom)
        {
            bottomWall = Instantiate(horizontalWallPrefab, transform.position + new Vector3(0, -10, 0), Quaternion.identity, transform);

        }

        if (!grid.ContainsKey(left) || !grid[left].hasRoom)
        {
            leftWall = Instantiate(veritcalWallPrefab, transform.position + new Vector3(-10, 0, 0), Quaternion.identity, transform);
        }

        if (!grid.ContainsKey(right) || !grid[right].hasRoom)
        {
            rightWall = Instantiate(veritcalWallPrefab, transform.position + new Vector3(10, 0, 0), Quaternion.identity, transform);
        }

    }

    public void RemoveWallFacing(Vector2Int direction)
    {
        if (direction == Vector2Int.up && topWall) Destroy(topWall);
        if (direction == Vector2Int.down && bottomWall) Destroy(bottomWall);
        if (direction == Vector2Int.left && leftWall) Destroy(leftWall);
        if (direction == Vector2Int.right && rightWall) Destroy(rightWall);
    }
}
