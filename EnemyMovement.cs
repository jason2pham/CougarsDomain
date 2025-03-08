using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Transform pointA; // The first spot the enemy moves to
    public Transform pointB; // The second spot the enemy moves to
    public Transform player; // The player character the enemy might chase
    public float speed = 3f; // How fast the enemy moves
    public float chaseRange = 5f; // How close the player needs to be for the enemy to start chasing

    private Vector3 target; // The current point the enemy is moving toward
    private bool isChasing = false; // Keeps track of whether the enemy is chasing the player

    void Start()
    {
        // Start by moving toward point A
        target = pointA.position;
    }

    void Update()
    {
        // Check how far the enemy is from the player
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        Debug.Log("Distance to Player: " + distanceToPlayer); // Print distance for debugging

        // If the player is close, start chasing
        if (distanceToPlayer < chaseRange)
        {
            isChasing = true;
        }
        else
        {
            // If the player moves too far away, go back to patrolling
            isChasing = false;
        }

        // Decide what the enemy should do: chase or patrol
        if (isChasing)
        {
            ChasePlayer();
        }
        else
        {
            Patrol();
        }
    }

    void Patrol()
    {
        // Move toward the current patrol target (point A or B)
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

        // If the enemy reaches the patrol point, switch to the other one
        if (Vector3.Distance(transform.position, target) < 0.2f)
        {
            target = (target == pointA.position) ? pointB.position : pointA.position;
        }
    }

    void ChasePlayer()
    {
        Debug.Log("Chasing Player!"); // Print message to confirm chasing
        Vector3 direction = (player.position - transform.position).normalized; // Find direction to the player
        transform.position += direction * speed * Time.deltaTime; // Move toward the player
    }
}
