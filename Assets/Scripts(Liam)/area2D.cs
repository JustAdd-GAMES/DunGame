using UnityEngine;

public class area2D : MonoBehaviour
{
    private bool playerInside = false;
    private basicRoom parentRoom;

    void Start()
    {
        parentRoom = GetComponentInParent<basicRoom>();
        if (parentRoom == null)
        {
            Debug.LogError("No room script found on parent");
        }
    }

    void Update()
    {
        if (playerInside && Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Spacebar Pressed");
            if (parentRoom != null)
            {
                parentRoom.SpawnRooms();
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = false;
        }
    }
}
