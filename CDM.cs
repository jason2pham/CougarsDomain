// Sound program for CougarsDomain Navid Nafeie CS 320

using UnityEngine;

public class PlayMusic : MonoBehaviour {

    public AudioClip musicClip;  // Drag music files 
    private AudioSource audioSource;

    void Start() {
    
      
        audioSource = gameObject.AddComponent<AudioSource>();

        audioSource.clip = musicClip;
        
        audioSource.loop = true; // Play and loop through music 
        audioSource.Play();
    }
}

