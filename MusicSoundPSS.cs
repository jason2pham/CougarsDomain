// Navid Nafeie
// CS 320 Midterm
// Weekly Progress 9



using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicSound : MonoBehaviour
{
    // Sound effects and movement and physics variables
    public AudioClip movementSound;
    public AudioClip jumpingSound1;
    public AudioClip jumpingSound2;
    private AudioSource audioSource;
    public Vector3 lastPosition;
    public float jumpForce = 5.0f;
    public float moveSpeed = 5f;
    private Rigidbody rb;
    public int jumpCount = 0;
    public AudioClip landingSound;
    public AudioClip fallingSound;
    public float fallingThreshold = -11f;
    public AudioClip gameOverSound;
    public bool isGameOver = false;
    public AudioClip collisionSound; 
    

    void Awake()
    {
        DontDestroyOnLoad(this); // Object persists between scenes
        audioSource = GetComponent<AudioSource>(); // AudioSource component
        rb = GetComponent<Rigidbody>(); // Rigidbody component
        lastPosition = transform.position; // Gets last position for movement tracking
    }


    public void Update()
    {
        if (isGameOver) return; // If the game is over, stop all updates 
        
        // Get movement input
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontal, 0, vertical) * moveSpeed;
        rb.linearVelocity = new Vector3(movement.x, rb.linearVelocity.y, movement.z);

        // Play movement sound when object is moving
        if (transform.position != lastPosition)
        {

            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(movementSound);
            }

            lastPosition = transform.position;
        }

        // Jumps for object
        if (Input.GetKeyDown(KeyCode.Space) && jumpCount < 2)
        {
            rb.linearVelocity =
                new Vector3(rb.linearVelocity.x, 0,
                    rb.linearVelocity.z); // Resets vertical velocity before jumping so object can do a jump again
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

            // Play a certain sound based on jump count
            if (jumpCount == 0)
            {
                audioSource.PlayOneShot(jumpingSound1);
            }
            else if (jumpCount == 1)
            {
                audioSource.PlayOneShot(jumpingSound2);
            }

            jumpCount++;

        }

        // Detection for object if it falls
        if (transform.position.y < fallingThreshold)
        {
            audioSource.PlayOneShot(fallingSound);
        }

        if (transform.position.y < fallingThreshold && !isGameOver)
        {
            GameOver();
            
        }
    }

    private void GameOver()
    {
        isGameOver = true;
        audioSource.PlayOneShot(gameOverSound);
        rb.linearVelocity = Vector3.zero;
        rb.isKinematic = true;
        Invoke("GameOversound", 2.5f);
    }

    private void GameOversound()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }



    private void OnCollisionEnter(Collision collision)
    {
        if (collisionSound != null)
        {
            audioSource.PlayOneShot(collisionSound);
        }
        // Reset jump count and play a landing sound when touching the ground
        if (collision.gameObject.tag == ("Ground"))
        {
            jumpCount = 0;
            audioSource.PlayOneShot(landingSound);
        }
    }

 }





 




