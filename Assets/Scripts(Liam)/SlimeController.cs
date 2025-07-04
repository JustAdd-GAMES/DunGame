using UnityEngine;

public class SlimeController : MonoBehaviour
{
    private GameObject player;
    private Vector2 targetPoint;
    private bool movingtoTarget = false; // random point in player vicinity
    private bool movingtoPlayer = false; // straight to player

    private Rigidbody2D rb;
    private CircleCollider2D slimeCollider;

    private float moveSpeed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        slimeCollider = GetComponent<CircleCollider2D>();

        var behaviour = GetComponent<SlimeBehavior>();
        if (behaviour && behaviour.slimeData != null)
        {
            moveSpeed = behaviour.slimeData.EnemySpeed;
        }
        else
        {
            Debug.Log("SlimeController doesn't have SlimeBehaviour");
        }

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (movingtoTarget)
        {
            MoveTowards(targetPoint);
            if (Vector2.Distance(transform.position, targetPoint) < 0.1f)
            {
                movingtoTarget = false;
                movingtoPlayer = true;
            }
        }
        else if (movingtoPlayer && player != null)
        {
            MoveTowards(player.transform.position);
        }

    }

    void MoveTowards(Vector2 destination) 
    {
        Vector2 direction = (destination - (Vector2)transform.position).normalized;
        rb.MovePosition(rb.position + direction * moveSpeed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (player != null) return;

        if (other.CompareTag("Player"))
        {
            player = other.gameObject;
            CircleCollider2D playerCol = other.GetComponent<CircleCollider2D>();
            if (playerCol)
            {
                float angle = Random.Range(0f, 2 * Mathf.PI);
                float radius = Random.Range(0f, playerCol.radius);
                Vector2 offset = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
                targetPoint = (Vector2)player.transform.position + offset;

                movingtoTarget = true;
            }
        }
    }
}
