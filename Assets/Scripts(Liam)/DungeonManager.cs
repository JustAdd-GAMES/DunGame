using UnityEngine;

public class DungeonManager : MonoBehaviour
{

    public GameObject[] roomsPrefab;
    public GameObject spawnRoom;
    public GameObject verticalHall;
    public GameObject horizontalHall;
    public GameObject verticalWall;
    public GameObject horizontalWall;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spawnRoom = roomsPrefab[0];
        verticalHall = roomsPrefab[1];
        horizontalHall = roomsPrefab[2];
        verticalWall = roomsPrefab[3];
        horizontalWall = roomsPrefab[4];
        DungeonGen();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void DungeonGen()
    {

        Instantiate(spawnRoom, Vector3.zero, Quaternion.identity);
        HallOrWall(spawnRoom);

    }

    // randomly decides which "entrance" of the rectangular room should have a hall or wall
    void HallOrWall(GameObject roomType)
    {

        rectangleData roomData = roomType.GetComponent<rectangleData>();
        int[] hallArray = new int[roomData.entrances];
        for (int i = 0; i < roomData.entrances - 1; i++) // cycles through however many openings there are
        {

            int openFlag = Random.Range(-3, 1);
            hallArray[i] = openFlag;

        }

        for (int j = 0; j < roomData.entrances; j++)
        {

            if (hallArray[j] == 1)
            {

                switch (j)
                {

                    case 0:
                        Instantiate(verticalWall, roomData.left_entrance, Quaternion.identity);
                        break;
                    case 1:
                        Instantiate(verticalHall, roomData.top_entrance, Quaternion.Euler(0, 0, 0));
                        break;
                    case 2:
                        Instantiate(horizontalHall, roomData.right_entrance, Quaternion.identity);
                        break;
                    case 3:
                        Instantiate(verticalHall, roomData.bottom_entrance, Quaternion.Euler(0, 0, 0));
                        break;
                    
                }
            }
            else
            {

                switch (j)
                {

                  case 0:
                        Instantiate(verticalWall, roomData.left_entrance, Quaternion.identity);
                        break;
                    case 1:
                        Instantiate(horizontalWall, roomData.top_entrance, Quaternion.Euler(0, 0, 0));
                        break;
                    case 2:
                        Instantiate(verticalHall, roomData.right_entrance, Quaternion.identity);
                        break;
                    case 3:
                        Instantiate(horizontalWall, roomData.bottom_entrance, Quaternion.Euler(0, 0, 0));
                        break;

                }

            }
        }

    }
}
