using UnityEngine;
using UnityEngine.AI; // Include this for the NavMesh components

public class EnemyAI : MonoBehaviour
{
    public Transform player; // Assign the player's transform in the inspector
    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        if (!player) // Find the player automatically if not assigned
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    void Update()
    {
        // Check if we're on the road
        if (IsOnRoad())
        {
            // Update the enemy's destination to be the player's position
            agent.SetDestination(player.position);
        }
    }

    bool IsOnRoad()
    {
        // Perform a Raycast down to check if we're above a 'Road' object
        RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up, 0.1f);
        if (hit.collider != null && hit.collider.CompareTag("Road"))
        {
            return true;
        }
        return false;
    }
}
