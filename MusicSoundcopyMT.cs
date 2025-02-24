// Navid Nafeie
// CS 320 Midterm
/*CougarsDomain is a project designed to allow students to create and customize their own characters to battle through waves of bosses. 
Each wave presents unique challenges,
culminating in a final boss who is inspired by one of the WSU Vancouver CS professors—reimagined as an animal! */

// This Script handles movement and jumping sounds for the player's character
// It plays sound effects when the player moves, jumps, and lands
// Background music is played as well

// Data includes audio clips, movement data, and physics components 



using UnityEngine;
// Featue MonoBehavior
// Used MonoBehaviour to allow script to interact with Unity's built-in systemts 
public class MusicSound : MonoBehaviour // base class, allows you to attach scripts to GameObjects in unity 
{

   // Declaring variables 
   // Public variables used to change values easily 
   // Private variables used to keep class focused on its specific tasks 
    public AudioClip movementSound; // Sound for movement 
    public AudioClip jumpingSound1; // Sound for jumping
    public AudioClip jumpingSound2; // Sound for a double jump
    private AudioSource audioSource; // Audio source component for playing sounds
    private Vector3 lastPosition; // Stores last position of object to detect movement 
    public float jumpForce = 5.0f; // Jump force applied when object is jumping
    public float moveSpeed = 5f; // Movement speed of the object 
    private Rigidbody rb; // Ridgebody component for the object's physics-based movement (handles phsyics)
    private int jumpCount = 0; // Tracks the number of jumps
    public AudioClip landingSound; // Sound for landing


    void Awake() // Unity needs to call Awake when script is being loaded
    {
        DontDestroyOnLoad(this); // Checks the object persists when going through scene changes
        audioSource = GetComponent<AudioSource>(); // Gets the AudioSource component 
        rb = GetComponent<Rigidbody>(); // Gets the RigidBody component for physics interactions
        lastPosition = transform.position; // Initializes last position to the starting position
    }

    void Update() // Function for implementation of the game script
    {
        
        // Player input, capture input with arrow keys 
        float horizontal = Input.GetAxis("Horizontal"); //GetAxis used to get player input for movement, returns float value between left (-1) and right (1)
        float vertical = Input.GetAxis("Vertical"); // Returns float between down (-1) and up (1)
        
        // Create movement vector based on input, makes object move
        Vector3 movement = new Vector3(horizontal, 0, vertical) * moveSpeed; // Struct used (Vector3)
        rb.linearVelocity = new Vector3(movement.x, rb.linearVelocity.y, movement.z);

        // If the object is moving, play movement sound
        if (transform.position != lastPosition) // Check if current position is different from stored position 
        {

            if (!audioSource.isPlaying) // Check if audio is not playing
            {
                audioSource.PlayOneShot(movementSound); // Play movement sound 
            }

            lastPosition = transform.position; // Update last positon 
        }
        
        // If spacebar is pressed, object will jump play a sound and if the object does a double jump, play another sound
        if (Input.GetKeyDown(KeyCode.Space) && jumpCount < 2)
        {
            // Reset vertical velocity, makes the object jump
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z);
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

            // Play the correct jump sound based on the type of jump the object makes
            if (jumpCount == 0) // one jump
            {
                audioSource.PlayOneShot(jumpingSound1); // Play sound for one jump 
            }
            else if (jumpCount == 1) // second jump
            {
                audioSource.PlayOneShot(jumpingSound2); // Play the sound for double jump 
            }
            jumpCount++; // Increment jump count
            
        }
    }
    private void OnCollisionEnter(Collision collision) // OnCollisionEnter used when rigidbody (our object) touches something else, handles collision detection 
    {
        if (collision.gameObject.tag == ("Ground")) // Reset jump count when the object touches the ground
        {
            jumpCount = 0; // Reset jump once landed 
            audioSource.PlayOneShot(landingSound); // Play landing sound when the object lands 
        }
    }

}

 




