/*
Shaheen Nafeie Pr10 2/3
*/

/* This is a GameManager (singleton) that can be accesed in our game globally. 
*/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class GameManager : MonoBehaviour
{

	public static GameManager gameManager{ get;  private set; } //instance, can be accsesed using .gameManager.
	//Above we make use of get,set. Any piece of code can read gameManager but only GameManager class can modify. (private)

	public systemhealth userHealth =  new systemhealth(100, 100); //new instance of systemhealth. current health is 100, and has a maximum of 100 health. 
	
    void Start(){
	
		if(gameManager !=null && gameManager != this){
			Destroy(this); //if a game manager exists but it is a different one, we need to destory this one
		}
		else{
			gameManager=this; //otherwise, set gameManager equal to this instance
		}
	
	}
}
