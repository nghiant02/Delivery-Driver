using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public Transform player;
    public float moveSpeed = 6f;
    public float checkDistance = 1f;
    private Rigidbody2D rb;

    private float timeSinceLastDecision = 0f;
    private float decisionInterval = 0.5f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (!player)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    void Update()
    {
        if (Time.time - timeSinceLastDecision > decisionInterval)
        {
            MakeMovementDecision();
            timeSinceLastDecision = Time.time;
        }
    }

    void FixedUpdate()
    {
        rb.velocity = transform.up * moveSpeed;
    }

    private void MakeMovementDecision()
    {
        if (!IsRoadAhead() || !IsOnRoad())
        {
            DecideDirectionBasedOnPlayer();
        }
    }

    private bool IsRoadAhead()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, checkDistance);
        return hit.collider != null && hit.collider.CompareTag("Road");
    }

    private bool IsOnRoad()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up, checkDistance);
        return hit.collider != null && hit.collider.CompareTag("Road");
    }

    private void DecideDirectionBasedOnPlayer()
    {
        Vector3 directionToPlayer = player.position - transform.position;
        float angleToPlayer = Vector2.SignedAngle(transform.up, directionToPlayer);

        if (Mathf.Abs(angleToPlayer) > 90) // If the player is mostly behind the enemy
        {
            // Choose to turn left or right randomly if directly going towards the player isn't an option
            float randomTurn = Random.value < 0.5f ? 90 : -90;
            transform.Rotate(0, 0, randomTurn);
        }
        else
        {
            // Turn towards the player by determining if they are to the left or right
            float turnDirection = angleToPlayer > 0 ? -90 : 90;
            transform.Rotate(0, 0, turnDirection);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player Loses!");
        }
        else if (!collision.gameObject.CompareTag("Road"))
        {
            // If it collides with something that's not the road, adjust direction
            DecideDirectionBasedOnPlayer();
        }
    }
}
