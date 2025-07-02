using UnityEngine;
using System.Collections.Generic;

public class basicRoom : MonoBehaviour
{
    public Transform[] exits;
    public int entrancesTaken = 0;
    public const int maxEntrances = 2;
    public enum Direction { Top = 0, Right = 1, Bottom = 2, Left = 3 };
    public Direction? entranceDirection = null;
    public GameObject[] roomPrefabs;
    private bool hasSpawned = false;
    private bool[] exitsOccupied = new bool[4];

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SpawnRooms()
    {
        if (hasSpawned) return;
        hasSpawned = true;

        int entrancesToCreate = maxEntrances - entrancesTaken;
        if (entrancesToCreate <= 0) return;

        // List of all 4 directions
        List<int> availableExits = new List<int>();

        for (int i = 0; i < 4; i++)
        {
            if (entranceDirection.HasValue && i == (int)entranceDirection.Value) continue;
            if (exitsOccupied[i]) continue;

            availableExits.Add(i);
        }

        if (availableExits.Count == 0) return;

        entrancesToCreate = Mathf.Min(entrancesToCreate, availableExits.Count);

        for (int i = 0; i < entrancesToCreate; i++)
        {
            int index = Random.Range(0, availableExits.Count);
            int dir = availableExits[index];
            availableExits.RemoveAt(index);

            SpawnRoomAt((Direction)dir);
            exitsOccupied[dir] = true;
            entrancesTaken++;
        }
    }

    private void SpawnRoomAt(Direction direction)
    {
        Transform spawnPoint = exits[(int)direction];

        GameObject newRoom = Instantiate(roomPrefabs[0], spawnPoint.position, Quaternion.identity);

        basicRoom roomScript = newRoom.GetComponent<basicRoom>();
        if (roomScript != null)
        {
            //Tells the new room which direction it came from
            roomScript.entranceDirection = GetOppositeDirection(direction);
        }
    }

    private Direction GetOppositeDirection(Direction direction)
    {
        switch (direction)
        {
            case Direction.Top: return Direction.Bottom;
            case Direction.Right: return Direction.Left;
            case Direction.Bottom: return Direction.Top;
            case Direction.Left: return Direction.Right;
            default: return Direction.Top; // shouldn't happen but just incase
        }
    }

}
