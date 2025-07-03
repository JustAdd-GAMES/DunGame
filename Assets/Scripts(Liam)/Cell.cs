using UnityEngine;

public class Cell : MonoBehaviour
{
    public Vector2Int gridPosition;
    public bool hasRoom = false;
    public bool entered = false;
    public bool roomSpawned = false;

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
            Debug.Log("Entered room at " + gridPosition);
        }
    }
}
