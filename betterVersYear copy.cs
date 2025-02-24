using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/*
Shaheen Nafeie
Midterm Milestone
CS 320
02/23/25
This is the second iteration of the drop down menu. It makes use of TextMeshPro which is in Unity.
The values here are 0 index based. 

*/


public class betterVersYear : MonoBehaviour
{
    public TextMeshProUGUI select; //Reference to TMP. 

    public void ReadSelection(int value) //This method prompts one of four messages to the game screen dependent upon what the user selects.
    {
        if (value == 0){
            select.text = "Freshman selected!"; //message goes to screen if freshman is selected
        }

         else if (value == 1){
            select.text = "Sophomore selected!"; //message goes to screen if sophomore is selected
        }

         else if (value == 2){
            select.text = "Junior selected!"; //message goes to screen if junior is selected
        }

         else if (value == 3){
            select.text = "Senior selected!"; //message goes to screen if senior is selected. 
        }
    }

  
 
}
