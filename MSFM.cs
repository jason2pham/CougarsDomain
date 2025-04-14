//Navid Nafeie CS 320 Final Milestone

using UnityEngine; // Allows usage of Rigidbody, AudioSource, GameObject
using UnityEngine.SceneManagement; // Transitions of different scenes

public class MusicSound : MonoBehaviour // The base class of unity, allowing the usage of Start(), Update(), Awake()
{
    // Movement and jumping
    public float jumpForce = 5.0f; // The Strength of the jump force
    public float moveSpeed = 5f; // The speed of the player movement
    private Rigidbody rb; // Reference to the player's Rigidbody (Control object by physics: moving, jumping, falling, respawn)
    public Vector3 lastPosition; // Tracks last position to detect movement(stores direction using floats x, y, and z)
    public int jumpCount = 0; // Limits player to double jump (Max is 2 jumps)
    // Audio clips
    private AudioSource audioSource; // The component that plays sound (Speaker)
    
    // AudioClip are the sound files (Stores .wav or .mp3)
    public AudioClip movementSound; // Stores audio for object movement 
    public AudioClip jumpingSound1; // Stores audio for first jump
    public AudioClip jumpingSound2; // Stores audio for second jump 
    public AudioClip landingSound; // Stores audio for when object lands
    public AudioClip fallingSound; // Stores audio for when object falls down from plane
    public AudioClip gameOverSound; // Stores audio when player loses (Object falls down from plane)
    public AudioClip collisionSound; // Stores audio for when object hits another object)
    public AudioClip comboSound; // When two or more hits happen, it is a combo. This stores the audio for a combo
    public AudioClip summonSound; // Stores sound for respawn
    public AudioClip enemyFallingSound; // Stores sound for when enemy falls from plane
    public AudioClip winSound; // Stores sound for when player wins (Enemy object falls from plane)

    // Game state
    public bool isGameOver = false; // Flag to check if game is over
    public float fallingThreshold = -11f; // If player falls below this, trigger game over 

    // Combo system
    private int comboCounter = 0; // Tracks consecutive hits to see if combo has been done 
    private float comboTimer = 0f; // A timer to reset combo status
    public float comboReset = 1.5f; // Time window to keep combo status active

    // Enemy tracking
    public GameObject enemyCube; // Reference to enemy object (A cube)
    private Vector3 enemyPosition; // Original position to respawn enemy 
    public float enemyFallThreshold = -11f; // If enemy falls below this, triggers respawn
    private bool enemyFallingSoundCheck = false; // Prevents duplicate sound triggers, should happen once per fall 

    void Awake()
    {
        // Makes sure GO isn't destroyed when loading a new scene
        // Makes it so music & sounds continue across scenes
        DontDestroyOnLoad(this); 
        
        // Get the AudioSource component attached to this GO
        // Used to play all sound effects
        audioSource = GetComponent<AudioSource>();
        
        // Get Rigidbody component attached to this GO
        // Control player's physics-based momement
        rb = GetComponent<Rigidbody>();

        // Store current position of the player as the last know position
        // Used later to detect if player has moved for playing movement sounds
        lastPosition = transform.position;

        // Check if the enemy cube GO is assigned in the Inspector on unity
        // If it is, save its starting position so we can respawn this GO if it falls off
        if (enemyCube != null)
        {
            enemyPosition = enemyCube.transform.position;
        }
    }

    void Update()
    {
        // If game is over, skipp all logic below and do nothing in this frame
        // This will prevent player control or sound effects from triggering once game is over
        if (isGameOver) return; 

        // Check for player input and move the player object (Input from arrow keys)
        // Also plays movement sound when appropriate 
        HandleMovement(); 
        
        // Checks if player is pressing jump key and apply jumping logic
        // Will handle double-jump limit and will play jump sounds
        HandleJumping(); 
        
        // Checks if player has fallen below the threshold
        // If so, trigger falling and game over sounds and logic
        HandleFalling(); 
        
         // Updates the timer that teacks how long a combo hit remains active
        // Resets the combo if time runs out
        HandleComboTimer(); 
        
        // Monitor's enemy position
        // If the enemy falls of plane, trigger enemy falling sound, win logic, and respawn
        CheckEnemyFalling();
    }

   private void HandleMovement()
   {
    // Get horizontal input (left/right arrow keys or A/D keys)
    float horizontal = Input.GetAxis("Horizontal"); 

    // Get vertical input (up/down arrow keys or W/S keys)
    float vertical = Input.GetAxis("Vertical"); 

    // Create a movement vector based on player input, only affecting X and Z axes (no vertical movement).
    // Multiply the vector by the movement speed to control how fast the player moves.
    Vector3 movement = new Vector3(horizontal, 0, vertical) * moveSpeed; 

    // Apply the movement to the Rigidbody by setting its linear velocity.
    // This preserves the current Y-axis (vertical) velocity so jumping/falling isn't interrupted.
    rb.linearVelocity = new Vector3(movement.x, rb.linearVelocity.y, movement.z); 

    // If the player's position has changed and no other audio is currently playing...
    if (transform.position != lastPosition && !audioSource.isPlaying) 
    {
        // Play the movement sound effect to indicate the player is moving.
        audioSource.PlayOneShot(movementSound);

        // Update the last known position so this logic doesn't repeat unnecessarily.
        lastPosition = transform.position;
    }
}


    private void HandleJumping()
    {
    // Check if the player presses the spacebar and hasn't yet reached the jump limit (double jump).
    if (Input.GetKeyDown(KeyCode.Space) && jumpCount < 2) 
    {
        // Reset the player's vertical velocity (Y-axis) before applying a new jump force.
        // This ensures that any residual upward or downward velocity is cleared before jumping.
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z); 

        // Apply an upward force to the player's Rigidbody to make them jump.
        // The force is based on the specified jump force and is applied as an impulse.
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

        // Check if it's the first jump (jumpCount == 0), and play the corresponding jump sound.
        if (jumpCount == 0)
            audioSource.PlayOneShot(jumpingSound1); // Play sound for first jump

        // If it's the second jump (jumpCount == 1), play a different sound for the double jump.
        else if (jumpCount == 1)
            audioSource.PlayOneShot(jumpingSound2); // Play sound for second jump (Double jump)

        // Increment the jump count to track how many jumps the player has performed.
        jumpCount++; // Increase jump count after a jump
    }
}


    private void HandleFalling()
    {
    // Check if the player's Y-position is lower than the defined falling threshold.
    // If the player falls below the threshold (e.g., falls off the platform), play the falling sound.
    if (transform.position.y < fallingThreshold) 
    {
        audioSource.PlayOneShot(fallingSound); // Play falling sound effect
    }

    // Check again if the player's Y-position is below the threshold and also ensure the game is not over.
    // If the player falls below the threshold and the game is still ongoing, trigger the game over logic.
    if (transform.position.y < fallingThreshold && !isGameOver) 
    {
        GameOver(); // Call the GameOver() method to end the game
    }
}


   private void HandleComboTimer()
   {
    // Check if there have been any combo hits (comboCounter > 0)
    if (comboCounter > 0) 
    {
        // Decrease the combo timer by the amount of time passed since the last frame.
        // Time.deltaTime represents the time in seconds between frames.
        comboTimer -= Time.deltaTime; 

        // If the timer runs out (it reaches 0 or below), reset the combo.
        if (comboTimer <= 0f)
        {
            comboCounter = 0; // Reset the combo count if the time window has expired
        }
    }
}


   private void CheckEnemyFalling()
   {    
    // Check if the enemy object exists (enemyCube is not null),
    // and ensure the falling sound hasn't been played yet (enemyFallingSoundCheck is false),
    // and check if the enemy's Y position is below the fall threshold (enemyFallThreshold).
    if (enemyCube != null && !enemyFallingSoundCheck && enemyCube.transform.position.y < enemyFallThreshold)
    {
        // Play the sound effect for when the enemy falls off the platform.
        audioSource.PlayOneShot(enemyFallingSound); 

        // Use Invoke to call the aWin() method after the length of the falling sound finishes,
        // signaling that the player has won after the enemy falls.
        Invoke("aWin", enemyFallingSound.length); 

        // Set the enemyFallingSoundCheck flag to true to prevent the sound from playing again.
        enemyFallingSoundCheck = true;

        // Use Invoke to call the summonEnemy() method after the enemy falling sound ends,
        // plus an additional 0.5 seconds delay to allow for sound completion before respawning.
        Invoke("summonEnemy", enemyFallingSound.length + 0.5f); 
    }
}


  private void summonEnemy()
  {
    // Reset the enemy's position to its initial position stored in enemyPosition.
    // This brings the enemy back to its starting location after falling off the platform.
    enemyCube.transform.position = enemyPosition;

    // Reset the enemy's Rigidbody velocity to zero to stop any ongoing movement.
    // This ensures the enemy doesn't continue moving when it respawns.
    enemyCube.GetComponent<Rigidbody>().linearVelocity = Vector3.zero;

    // Play the sound effect for respawning the enemy (summon sound).
    audioSource.PlayOneShot(summonSound); 

    // Reset the flag that tracks whether the enemy falling sound has been played.
    // This allows the falling sound to be triggered again if the enemy falls in the future.
    enemyFallingSoundCheck = false; 
}


  private void GameOver()
  {
    // Set the game over flag to true, indicating that the game is over.
    isGameOver = true; 

    // Play the game over sound to signal the end of the game.
    audioSource.PlayOneShot(gameOverSound); 

    // Stop the player's movement by setting the Rigidbody's velocity to zero.
    // This ensures the player doesn't continue moving after the game ends.
    rb.linearVelocity = Vector3.zero; 

    // Set the Rigidbody's isKinematic property to true, which disables physics interactions with the player.
    // This prevents the player from being affected by gravity or collisions once the game is over.
    rb.isKinematic = true; 

    // Use Invoke to call the GameOversound() method after a 2.5-second delay, triggering scene reload.
    Invoke("GameOversound", 2.5f);

    // Use Invoke to call the summonSoundForObject() method after a 2.5-second delay.
    // This plays the summon sound as part of the game over process.
    Invoke("summonSoundForObject", 2.5f); 
}


   private void GameOversound()
   {
    // Reload the current scene to restart the game.
    // This effectively resets the game state by loading the same scene again.
    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


   private void summonSoundForObject()
   {
    // Play the summon sound (this sound is for the player when they respawn).
    // It uses the AudioSource component to play the sound once.
    audioSource.PlayOneShot(summonSound);
    }


    public void aWin()
    {
    // Play the win sound when the enemy falls from the plane.
    // This sound is used to indicate the player has won.
    audioSource.PlayOneShot(winSound);
    }


    private void OnCollisionEnter(Collision collision)
    {
    // Play collision sound if a collision occurs and collisionSound is not null.
    // This ensures that whenever the player collides with something, a sound is played.
    if (collisionSound != null)
    {
        audioSource.PlayOneShot(collisionSound);
    }

    // Reset jump count and play a landing sound when the player lands on the ground.
    // This condition checks if the player has collided with an object tagged as "Ground".
    if (collision.gameObject.CompareTag("Ground"))
    {
        jumpCount = 0; // Reset the jump count to allow jumping again.
        audioSource.PlayOneShot(landingSound); // Play a sound to indicate the player has landed.
    }

    // Register combo hits when the player collides with objects tagged as "Enemy" or "ComboTarget".
    // This checks for collision with enemy objects or combo targets to track combo hits.
    if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("ComboTarget"))
    {
        ComboHit(); // Call ComboHit to track consecutive hits for combos.
    }
}

    private void ComboHit()
    {   
    // Increase the combo counter by 1 to track consecutive hits.
    // Each time a valid combo hit occurs (e.g., hitting an enemy), the combo counter increases.
    comboCounter++;

    // Reset the combo timer to the specified duration (comboReset).
    // This ensures that the combo timer is restarted every time a hit occurs within the combo window.
    comboTimer = comboReset;

    // If the combo counter reaches or exceeds 2, it means the player has performed a combo.
    // Play the combo sound to notify the player that they have achieved a combo.
    if (comboCounter >= 2)
    {
        audioSource.PlayOneShot(comboSound); // Play the combo sound when two or more hits are made in quick succession.
    }
  }

}






 




