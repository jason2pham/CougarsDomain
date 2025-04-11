// Navid Nafeie
// CS 320
// Weekly Progress 2

// This script makes it so an object has some sort of noise while it is moving!


using UnityEngine;

public class MovingCubeWithSound : MonoBehaviour
{
    public float speed = 5f;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        
        float horizontal = Input.GetAxis("Horizontal"); // When the cube is moving left or right, it will make a bubble noise. 
        Vector3 movement = new Vector3(horizontal, 0, 0) * speed * Time.deltaTime;
        transform.Translate(movement);
        
        if (horizontal != 0 && !audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }
}