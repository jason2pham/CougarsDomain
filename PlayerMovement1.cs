using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float walkSpeed = 5f; // Normal movement speed
    public float sprintSpeed = 10f; // Speed while sprinting
    public Rigidbody rb; // Reference to the Rigidbody component
    private Vector3 moveDirection; // Stores movement input
    private bool isSprinting = false; // Tracks if sprint key is held

    void Start()
    {
        // Get Rigidbody component attached to player
        rb = GetComponent<Rigidbody>();

        // Ensure Rigidbody settings are correct
        rb.useGravity = true; // Gravity should be enabled
        rb.isKinematic = false; // Allow physics movement
        rb.freezeRotation = true; // Prevent tipping over
    }

    void Update()
    {
        // Check if Shift key is held
        isSprinting = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
        MoveInput(); // Get player input
    }

    void FixedUpdate()
    {
        Move(); // Apply movement
        PreventSinking(); // Keep player above ground
    }

    void MoveInput()
    {
        float moveX = Input.GetAxis("Horizontal"); // A/D or Left/Right Arrow
        float moveZ = Input.GetAxis("Vertical");   // W/S or Up/Down Arrow

        // Create movement vector (Y is 0 to prevent jumping issues)
        moveDirection = new Vector3(moveX, 0f, moveZ).normalized;
    }

    void Move()
    {
        float currentSpeed = isSprinting ? sprintSpeed : walkSpeed;

        // Apply movement using Rigidbody velocity
        rb.velocity = new Vector3(moveDirection.x * currentSpeed, rb.velocity.y, moveDirection.z * currentSpeed);
    }

    void PreventSinking()
    {
        // Ensures player stays at ground level (adjust Y as needed)
        if (transform.position.y < 1)
        {
            transform.position = new Vector3(transform.position.x, 1, transform.position.z);
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z); // Stop downward force
        }
    }
}
