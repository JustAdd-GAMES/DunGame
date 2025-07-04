using UnityEngine;
using Unity.Cinemachine;

public class CameraManager : MonoBehaviour
{

    public Transform player;
    public float roomWidth = 20f;
    public float roomHeight = 20f;
    public float transitionSpeed = 5f;

    private Vector3 targetPosition;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        targetPosition = transform.position;
    }

    void Awake()
    {
    }

    // Update is called once per frame
    void Update()
    {
        FollowPlayer();
    }

    public void FollowPlayer()
    {
        Vector3 delta = player.position - targetPosition;

        if (Mathf.Abs(delta.x) > roomWidth)
        {
            int stepX = (delta.x > 0) ? 2 : -2;
            targetPosition += new Vector3(stepX * roomWidth, 0, -10);
        }

        if (Mathf.Abs(delta.y) > roomHeight)
        {
            int stepY = (delta.y > 0) ? 2 : -2;
            targetPosition += new Vector3(0, stepY * roomHeight, -10);
        }

        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * transitionSpeed);

        
    }

}
