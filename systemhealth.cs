/*
Shaheen Nafeie
PR10 3/3
This code creates the overall logic for our health system. 
*/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class systemhealth //class is responsible for creating our health system, it tracks the current health as well as the max health

{
    private int currentHealth; //stores current health
	private int maxHealth; //stores max health 
	
	public int myHealth{ //used to access or update the current health
		
		get{
			
			return currentHealth; //returns current health
		}
		set{
			currentHealth = value; //sets the current health to whatever the value is at that time
		}
	}
	//use get set here as well
	
	
	public int myMaxHealth{ //used to access or update the max health
		
		get{
			return maxHealth; //returns the max health
		}
		set{
			maxHealth = value; //sets the current health to whatever the value is at that time
		}
	}
	
	public systemhealth(int health, int healthmax){
		
		currentHealth= health;
		maxHealth = healthmax;
		
	}
	//Above is our constructor, it initalizes the current and max health

	public void Damage(int num){
		if(currentHealth > 0){
			currentHealth -=num;
		}
	} //in charge of depleting health when necessary
	
	public void Heal(int num){
		if(currentHealth < maxHealth){
			currentHealth +=num;
		}
		if(currentHealth > maxHealth){
			currentHealth = maxHealth;
		} //in charge of increasing health when necessary, if it goes over, we just set it equal to the max health
	}
	
	
}
