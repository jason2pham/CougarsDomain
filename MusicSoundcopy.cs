// Navid Nafeie
// Weekly Progress 4

// This script adds background music to a project on unity and adds sound whenever the object is moving




using UnityEngine;

public class MusicSound : MonoBehaviour
{
    public AudioClip movementSound; // Sound when object is moving 
    private AudioSource audioSource; // AudioSource component to play the sound
    private Vector3 lastPosition; // Keep track of the last position to detect movement of object

    public float moveSpeed = 5f; // Speed of object

    void Awake()
    {
        DontDestroyOnLoad(this); // Object won't be destoyed when loading a new scene
        audioSource = GetComponent<AudioSource>(); //AudiSource attached to object
        lastPosition = transform.position; // Store initial position
    }

    void Update()
    {
     
        float horizontal = Input.GetAxis("Horizontal"); // Arrow key movement 
        float vertical = Input.GetAxis("Vertical"); 

        Vector3 movement = new Vector3(horizontal, 0, vertical) * moveSpeed * Time.deltaTime;
        transform.Translate(movement); // Move object in unity itself

   
        if (transform.position != lastPosition)
        {
       
            if (!audioSource.isPlaying) // Play movement sound when object is moving
            {
                audioSource.PlayOneShot(movementSound); // Play movement sound
            }

            lastPosition = transform.position; // Update last position to the current positon
        }
    }
}




