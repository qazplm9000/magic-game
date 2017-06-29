using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour {

	public int maxHealth = 100;
	public int curHealth = 100;



	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		//prevents invalid health values
		verifyCurHealth();
		maxHealthAlwaysPositive();
	}

	void OnGUI(){
		float healthbarWidth = Screen.width/2 * (curHealth / (float)maxHealth);

		Debug.Log(healthbarWidth);

		GUI.Box(new Rect(10, 10, healthbarWidth / 2, 20), 
			curHealth + "/" + maxHealth);
	}

	void verifyCurHealth(){
		//makes sure the current health is never below 0 and never above maxHealth
		if(curHealth < 0){
			curHealth = 0;
		}

		if(curHealth > maxHealth){
			curHealth = maxHealth;
		}
	}

	void maxHealthAlwaysPositive(){
		//makes sure the maximum health is always at least 1
		if(maxHealth < 1){
			maxHealth = 1;
		}
	}
}
