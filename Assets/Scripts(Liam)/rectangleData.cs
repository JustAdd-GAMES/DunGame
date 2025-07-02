using UnityEngine;

public class rectangleData : MonoBehaviour
{
    public int entrances = 4;
    // hall spawn positions for rectangle room
    public Vector3 left_entrance = new Vector3(-23.7f, 0, 0);
    public Vector3 right_entrance = new Vector3(25.7f, 0, 0);
    public Vector3 top_entrance = new Vector3(0, 16.8f, 0);
    public Vector3 bottom_entrance = new Vector3(0, -14.5f, 0);

    // wall spawn positions for rectangle room
    public Vector3 left_wall = new Vector3(-16.36f, 0, 0);
    public Vector3 right_wall = new Vector3(16.36f, 0, 0);
    public Vector3 top_wall = new Vector3(0, 7.2f, 0);
    public Vector3 bottom_wall = new Vector3(0, -7.2f, 0);


}
