/*
Shaheen Nafeie
CS 320
Weekly Progress 2 

Week 1 consisted of setting up GitHub and getting used to it. I also 
spent some time learning C#. Pushed a simple C# file that consists of a basic menu.

This week consisted of learning how Unity works. I spent some time trying to get it 
to work on my machine and it finally does. I made 2 menu screens. One is a simple Start/options/quit
menu and the other menu asks if the user is a student. I spent some time trying to link the
two together but could not get it to work. My goal for next week is to have a fully interactive menu consisting
of the Start, Options, and Quit buttons. 

The C# code below is an attempt at getting the buttons to work. I dont think the issue
resides here but rather the way my objects are setup on Unity. 


*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class startlogic : MonoBehaviour
{

    public GameObject startMenu; // object for the start menu 
    public GameObject yesNo; // object for the yes/no panel

    // Start is called before the first frame update
    void Start()

    {
        yesNo.SetActive(false); // hides panel until i click start
    }

    // method when start is clicked
    public void OpenStart()

    {   startMenu.SetActive(false); //hides menu when start is clicked
        yesNo.SetActive(true); // shows yes/no panel when start is clicked
    }
    // method when yes is clicked
    public void Yes(){
        Debug.Log("hit yes"); //debug for unity to ensure yes is clicked
        yesNo.SetActive(false); //hides screen after yes is clicked
    }

    // method when no is clicked

    public void No(){
        Debug.Log("hit no"); //debug for unity to ensure no is clicked
        yesNo.SetActive(false); //hides screen after no is clicked
        startMenu.SetActive(true); //goes back to start menu if user hits no
    }
}
