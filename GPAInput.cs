/*
Shaheen Nafeie
CS 320
Weekly Progress 3

This week focused on creating a fully functional menu. This is a reattempt at it
as when trying to push my work to the repo, all my work got deleted. But I was able to create
it again. We have a main menu screen that has a start, options, and quit button. Hitting start sends us to a 
default game screen (no back button yet). Hitting options takes us to the options screen (no options yet). This screen also
includes a back button that sends us back to the main menu. Hitting quit does not do anything yet. But the game detects when user hits it.

When hitting start, before going to the game, it asks the user if they are a student or not. Hitting no does nothing yet. But hitting yes
prompts a text box for user to enter their GPA (no year selection yet).

Updates: hitting start doesnt go straight to game
Updates: is user student screen does include working back button(did not have this before deletion)
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GPAInput : MonoBehaviour
{
    private string grade; //string

    void Start()
    {
        
    }

   
    void Update()
    {
        
    }

    public void ReadGPA(string g){
        grade =g;
        Debug.Log(grade); //holds whatever GPA user enters (will add conditions to make sure user only enters GPA)
    }
}
