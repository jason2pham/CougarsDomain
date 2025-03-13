// Navid Nafeie CS 320 Testing Suite 

using NUnit.Framework;
using UnityEngine;


public class MusicSoundBBTest
{
    // Private fields 
    private GameObject testObject;
    private MusicSound musicSound;
    private AudioSource audioSource;

    // SetUp method runs 
    [SetUp]
    public void SetUp()
    {
        testObject = new GameObject("testObject"); // Create testObject
        testObject.AddComponent<Rigidbody2D>(); // Ridgidbody2d component 
        musicSound = testObject.AddComponent<MusicSound>(); // musicSound component 
        audioSource = testObject.AddComponent<AudioSource>(); // audioSource component
        musicSound.jumpingSound1 = AudioClip.Create("JumpingSound1", 1, 1, 44100, false);
        musicSound.movementSound = AudioClip.Create("movementSound", 1, 1, 44100, false);  
        
    }


    [Test] // Black-box test: testing the behavior of the sound playing when it should without knowing or interacting of my musicSound class
    public void TestJumpingSound()
    { 
        musicSound.jumpCount = 0; // Set jumpCount to 0 
        musicSound.Update(); // Update method call 
        Assert.IsTrue(audioSource.isPlaying, "Jump sound should play for one jump"); // audio source is playing for one jump
        Assert.AreEqual(musicSound.jumpingSound1,audioSource.clip); //  the correct jumping sound is set
        Assert.AreEqual(1, musicSound.jumpCount, "jump count should be incremented"); // jumpCount increased
        Assert.IsTrue(audioSource.time > 0, "Audio should start playing"); // sound is playing 
    }



    [Test] // White-box test: verifying the internal state of my musicSound object, checking if MovementSound holds a valid AudioClip
    public void TestMovementSound()
    { 
        AudioClip movementClip = musicSound.MovementSound; // Get value of MovementSound property from musicSound object
        Assert.IsNotNull(movementClip, "The movement sound should be assigned."); // movementClip is not null, movement sound should be assigned 
        Assert.IsInstanceOf<AudioClip>(movementClip, "The clip should be assigned."); // the clip is of expected type
        Assert.IsTrue(movementClip.samples > 0, "The clip should contain data"); // the clip is not empty
        Assert.IsTrue(movementClip.length > 0, "The clip duration is greater than 0"); // duration is greater than 0
    }

    [Test] // Integration test: testing interaction between movement of testObject, and sound played by musicSound
    public void TestMovementSoundPlayer()
    {
        testObject.transform.position = Vector3.zero; // Set position of testObject to (0,0,0)
        musicSound.lastPosition = Vector3.zero; // Set positon of musicSound to (0,0,0)
        testObject.transform.position = new Vector3(1, 0, 0); // Move testObject to new position (1, 0, 0)
        musicSound.Invoke("Update" , 0); // trigger sound update
        Assert.IsTrue(audioSource.isPlaying, "The movement sound should play when object moves."); //  audio source is playing movement sound after change of position
        Assert.IsNotNull(audioSource.clip, "The audio source should have a movement sound clip assigned."); // clip is assigned
        Assert.IsTrue(audioSource.volume > 0, "The volume should be greater than 0"); // volume is greater than 0
        Assert.AreNotEqual(Vector3.zero, testObject.transform.position, "The testObject should have moved."); // postion of the testObject has changed

    }
}