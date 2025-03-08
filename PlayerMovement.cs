using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Speed at which the player moves
    public float moveSpeed = 5f;

    // Reference to the Rigidbody component for physics-based movement
    public Rigidbody rb;

    // Stores the player's movement direction
    private Vector3 moveDirection;

    // Called once when the script is initialized
    void Start()
    {
        // Get and store the Rigidbody component attached to the player
        rb = GetComponent<Rigidbody>();
    }

    // Called once per frame to handle input
    void Update()
    {
        MoveInput(); // Get movement input
    }

    // Called at a fixed time interval to apply physics calculations
    void FixedUpdate()
    {
        Move(); // Move the player based on input
    }

    // Handles input from the player for movement
    void MoveInput()
    {
        // Get input from keyboard (WASD or Arrow Keys)
        float moveX = Input.GetAxis("Horizontal"); // Left (-1) to Right (+1)
        float moveZ = Input.GetAxis("Vertical");   // Down (-1) to Up (+1)

        // Create a movement vector, keeping Y as 0 (no vertical movement)
        moveDirection = new Vector3(moveX, 0f, moveZ).normalized;
    }

    // Applies the movement to the Rigidbody
    void Move()
    {
        // Set velocity based on movement direction and speed
        rb.linearVelocity = moveDirection * moveSpeed;
    }
}
