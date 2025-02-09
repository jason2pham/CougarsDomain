// Navid Nafeie
// CS 320
// Weekly Progress 2

/* This script ensures thar the GameObject it is attached to (.mp3 file) persists across all scene
 transitions in Unity. This is done by using the DontDestroyOnLoad function. This prevents the 
 GameObject from being destroyed when a new scene is loaded. */

using UnityEngine;

public class MusicP : MonoBehaviour // This class is used to manage the music across scene loads
{
    void musicAwake() // Method runs when game is tested via unity
    {
        DontDestroyOnLoad(this); // Will ensure all components persist between scene loads
    }
}

