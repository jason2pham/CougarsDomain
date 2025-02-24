
using UnityEngine;
using UnityEngine.UI;

//buggy 
/*
Shaheen Nafeie
Midterm milestone
02/23/2025
CS 320

Upon opening the project today, I have been dealing with some issues on Unity. One of them being a loading screen thats stuck at "inital asset database refresh".
A fix to this is to delete the temp and library folders prior to opening the project again. The downside to this is that it needs to re install those files and thats been taking some time.
Therefore I decided to work on a dropdown menu inside my main menu. This dropdown menu occurs when the user selects that they are a student. Inside the menu consists of the 4 grade levels.
This is one of two iterations that do almost the same thing. 

*/

public class Year : MonoBehaviour
{
    public Dropdown yearselect; // This refrences the dropdown UI element in Unity. I named it yearselect

   public void Start(){
        if(yearselect != null){ //checks to ensure the reference is not null
             yearselect.onValueChanged.AddListener(delegate {SelectAYear(); }); // This is a listener. This line calls the SelectAYear function whenever there is a change in value
           
        }

        else{
            Debug.LogError("error with dropdown"); //debug statment if there is an error with a dropdown. This error has been popping up from time to time. Sometimes it does and sometimes it doesnt.
        }
       
    }

    void fillDropDownButton(){
        if (yearselect != null){
            yearselect.ClearOptions();
            List<string> yearOps = new List<string> <string> { "freshman", "Sophomore", "junior", "senior"};
            yearselect.Addoptions(yearOps);
        }
    }

    /* This function dynamically initalizes the dropdown menu instead of soley relying on Unity.
        It clears any options that may be in the dropdown via unity, and adds the ones from the list onto
        the dropdown. This makes it easier to add options, and ensures the dropdown is always filled with stuff. 
    */
    public void SelectAYear(){ //This method runs when the user picks a year from the drop down menu

         if(yearselect == null){ 
            Debug.LogError("Error with dropdown"); //debug statement if the yearselect is missing(null)
            return;
        }

        /*if (yearselect.options.Count ==0){
            Debug.LogError("Error with dropdown");
            return; older debug statement 
        }
        */
        string yearChosen = yearselect.options[yearselect.value].text; //grabs the year the user selected 
        Debug.Log("Year that was chosen was: " + yearChosen); //prints this statement plus the year that was chosen into the debug consoke (wasnt popping up)
    }

    }
