// Navid Nafeie
// CS 320 Midterm
/*CougarsDomain is a project designed to allow students to create and customize their own characters to battle through waves of bosses. 
Each wave presents unique challenges,
culminating in a final boss who is inspired by one of the WSU Vancouver CS professors—reimagined as an animal! */

// This Script handles movement and jumping sounds for the player's character
// It plays sound effects when the player moves, jumps, and lands
// Background music is played as well

using UnityEngine;

public class MusicSound : MonoBehaviour // base class
{
   
    public AudioClip movementSound; // Sound for movement 
    public AudioClip jumpingSound1; // Sound for jumping
    public AudioClip jumpingSound2; // Sound for a double jump
    private AudioSource audioSource; // Audio source component for playing sounds
    private Vector3 lastPosition; // Stores last position of object to detect movement 
    public float jumpForce = 5.0f; // Jump force applied when object is jumping
    public float moveSpeed = 5f; // Movement speed of the object 
    private Rigidbody rb; // Ridgebody component for the object's physics-based movement 
    private int jumpCount = 0; // Tracks the number of jumps
    public AudioClip landingSound; // Sound for landing


    void Awake() // Unity needs to call Awake when script is being loaded
    {
        DontDestroyOnLoad(this); // Checks the object persists when going through scene changes
        audioSource = GetComponent<AudioSource>(); // Gets the AudiSource component 
        rb = GetComponent<Rigidbody>(); // Gets the RigidBody component for physics interactions
        lastPosition = transform.position; // Initializes last position to the starting position
    }

    void Update() // Function for implementation of the game script
    {
        
        // Player input 
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        
        // Create movement vector based on input
        Vector3 movement = new Vector3(horizontal, 0, vertical) * moveSpeed;
        rb.linearVelocity = new Vector3(movement.x, rb.linearVelocity.y, movement.z);

        // If the object is moving, play movement sound
        if (transform.position != lastPosition)
        {

            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(movementSound);
            }

            lastPosition = transform.position; // Update last positon 
        }
        
        // If spacebar is pressed, object will jump play a sound and if the object does a double jump, play another sound
        if (Input.GetKeyDown(KeyCode.Space) && jumpCount < 2)
        {
            // Reset vertical velocity 
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z);
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

            // Play the correct jump sound based on the type of jump the object makes
            if (jumpCount == 0)
            {
                audioSource.PlayOneShot(jumpingSound1);
            }
            else if (jumpCount == 1)
            {
                audioSource.PlayOneShot(jumpingSound2);
            }
            jumpCount++; // Increment jump count
            
        }
    }
    private void OnCollisionEnter(Collision collision) // OnCollisionEnter used when rigidbody (our object) touches something else 
    {
        if (collision.gameObject.tag == ("Ground")) // Reset jump count when the object touches the ground
        {
            jumpCount = 0;
            audioSource.PlayOneShot(landingSound); // Play landing sound when the object lands 
        }
    }

}

 




