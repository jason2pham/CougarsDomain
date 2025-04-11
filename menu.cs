/*
Shaheen Nafeie
CS 320
Weekly Progress 4

Couldn't get as much work done this week due to midterms. But this is
the C# script that is in charge of the menu and its scenes. The options button was done
on unity itself so there was no need for any scripting (yet). If we hit the start button
it asks the user if they are a student, loading a new scene. Quitting hit doesnt really do anything yet,
but Unity is recognizing that the user is clicking the hit button. My plans for the midterm presentation are to
include an option box for the user to select what year the student is in, maybe some options, and a difficulty option if the user
is not a student. 



* included a debug log for when the user hits the start button
* included an options function for when I add settings.


*/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; //in charge of scene managing

public class menu : MonoBehaviour
{
    public void StartButton(){
        Debug.Log("User hit start button"); //ensures user is hitting start button.
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1); //loads the scene
    }

    public void QuitButton(){
        Application.Quit();
        Debug.Log("User hit quit button"); //ensures user is hitting quit button and is being recognized.
    }

    public void Options(){
        Debug.Log("user hit options button"); //even though there is no code for the functionality of the options button, still good to make sure unity is detecting it
        //insert any options to add, language?, WASD or arrow keys?, cheat codes?
    }
    
}
