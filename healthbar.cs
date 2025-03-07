/*
Shaheen Nafeie
CS 320
Weekly Progress 6

This week, I decided to take a break from menu related stuff and work on a health bar.
This script sets up some basic functionallity for a healthbar. I want to work on more visually related
tasks next week. I have a basic health bar created in unity but it needs some work. 

*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class health: MonoBehaviour{
    public Image bar;  //image of health bar
    public float amount = 100f; //using a float for the amount of the health


void Update(){
    if(Input.GetKeyDown(KeyCode.Return)){ // checks if enter/return button is pressed, if so, do damage
    damage(10);
    }

    if(Input.GetKeyDown(KeyCode.Space)){ // checks if space bar is pressed, if so, heal
        healing(10);
    }

}

public void damage(float dam){
    amount -=dam; //subtract from health
    bar.fillAmount = amount / 100f; //update health bar when damaged
}

public void healing(float heal){
    amount +=heal;
    bar.fillAmount = amount / 100f; //adds to health
}


}
