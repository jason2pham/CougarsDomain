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
        DontDestroyOnLoad(this); // Ensures object presists across scene reloads, music and sounds will continue to play
        
        // Get referneces to components 
        audioSource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
        lastPosition = transform.position;

        // Store enemy's starting position
        if (enemyCube != null)
        {
            enemyPosition = enemyCube.transform.position;
        }
    }

    void Update()
    {
        if (isGameOver) return; // Skip this if game is over (An object falls down)

        HandleMovement(); // Manage movement and will play movement sound
        HandleJumping(); // Manage jumping and will play the jump sounds 
        HandleFalling(); // Detect player falling
        HandleComboTimer(); // Update combo logic
        CheckEnemyFalling(); // Check and handle enemy falling
    }

    private void HandleMovement()
    {
        float horizontal = Input.GetAxis("Horizontal"); // Gets horizontal input (left/right arrow keys)
        float vertical = Input.GetAxis("Vertical"); // Gets vertical input (up/down arrow keys)
        Vector3 movement = new Vector3(horizontal, 0, vertical) * moveSpeed; // Calculates movement vector for horizontal and vertical input, uses moveSpeed to control speed
        rb.linearVelocity = new Vector3(movement.x, rb.linearVelocity.y, movement.z); // Apply movement to Rigidbody 

        if (transform.position != lastPosition && !audioSource.isPlaying) // Plays the movement sound if position changes audio isn't playing
        {
            audioSource.PlayOneShot(movementSound);
            lastPosition = transform.position;
        }
    }

    private void HandleJumping()
    {
        if (Input.GetKeyDown(KeyCode.Space) && jumpCount < 2) // Jump logic with limit of 2 jumps (a double jump)
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z); // Reset vertical velocity before each jump 
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

            if (jumpCount == 0)
                audioSource.PlayOneShot(jumpingSound1); // Play sound for one jump
            else if (jumpCount == 1)
                audioSource.PlayOneShot(jumpingSound2); // Play sound for second jump (Double jump)

            jumpCount++; //Increase jump count
        }
    }

    private void HandleFalling()
    {
        if (transform.position.y < fallingThreshold) // Plays falling sound if player falls down from plane
        {
            audioSource.PlayOneShot(fallingSound);
        }

        if (transform.position.y < fallingThreshold && !isGameOver) // Once this happens its game over, game over sound will play
        {
            GameOver();
        }
    }

    private void HandleComboTimer()
    {
        if (comboCounter > 0) // Counts combo hits and will reduce timer
        {
            comboTimer -= Time.deltaTime; 
            if (comboTimer <= 0f)
            {
                comboCounter = 0; // Reset combo if time runs out
            }
        }
    }

    private void CheckEnemyFalling()
    {    
        // Checks if enemy has fallen, will play sound once
        if (enemyCube != null && !enemyFallingSoundCheck && enemyCube.transform.position.y < enemyFallThreshold)
        {
            audioSource.PlayOneShot(enemyFallingSound); // Play enemy falling sound 
            Invoke("aWin", enemyFallingSound.length); // Play win sound after fall 
            enemyFallingSoundCheck = true;
            Invoke("summonEnemy", enemyFallingSound.length + 0.5f); // Respawn ememy 
        }
    }

    private void summonEnemy()
    {
        // Reset enemy position and velocity
        enemyCube.transform.position = enemyPosition;
        enemyCube.GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
        audioSource.PlayOneShot(summonSound); // Play summon (Respawn) sound
        enemyFallingSoundCheck = false; // Reset sound check flag
    }

    private void GameOver()
    {
        isGameOver = true; // Set game over flag
        audioSource.PlayOneShot(gameOverSound); // Play game over sound
        rb.linearVelocity = Vector3.zero; // Stop player movement 
        rb.isKinematic = true; // Prevent physics from affecting player
        
        Invoke("GameOversound", 2.5f);
        Invoke("summonSoundForObject", 2.5f); // Play summon sound as a part of game over
    }

    private void GameOversound()
    {
        // Reload current scene to restart game
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void summonSoundForObject()
    {
        // Suumon sound plays (For player)
        audioSource.PlayOneShot(summonSound);
    }

    public void aWin()
    {
        // Play a win sound when enemy falls from plane
        audioSource.PlayOneShot(winSound);
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Play collisison sound
        if (collisionSound != null)
        {
            audioSource.PlayOneShot(collisionSound);
        }

        // Reset jump count and play a landing sound 
        if (collision.gameObject.CompareTag("Ground"))
        {
            jumpCount = 0;
            audioSource.PlayOneShot(landingSound);
        }

        // Register combo hits with enemies or targets 
        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("ComboTarget"))
        {
            ComboHit();
        }
    }
    private void ComboHit()
    {
        comboCounter++; // Increase combo count
        comboTimer = comboReset; // Reset combo timer 

        if (comboCounter >= 2)
        {
            audioSource.PlayOneShot(comboSound); // Play the combo sound when combo is high enough (After 2 consecutive hits)
        }
    }
}






 




