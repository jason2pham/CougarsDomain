/*
Shaheen Nafeie
Weekly PR 10 File 1/3
This is one of 3 files that is responsible for incorporating a health system.
This file is is in charge of testing the health by setting buttons for healing and damage.

*/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class behave : MonoBehaviour
{
    void Start(){
	
	}
	
	void Update(){
	
		if(Input.GetKeyDown(KeyCode.Space)){
			TakeDamage(5);
			Debug.Log(GameManager.gameManager.userHealth.myHealth);
		} //decreases health by 5 when hitting spacebar
		
		if(Input.GetKeyDown(KeyCode.H)){
			GainHeal(5);
			Debug.Log(GameManager.gameManager.userHealth.myHealth);
		} //increase health by 5 when hitting H
	
	}
	
	private void TakeDamage(int num){
	
		GameManager.gameManager.userHealth.Damage(num);
		//Debug.Log(GameManager.gameManager.userHealth.Health)
		
	
	} // Calls Damage to reduce health
	
	private void GainHeal(int num){
	
		GameManager.gameManager.userHealth.Heal(num);
		
	
	} // calls Heal to increase health.
}
