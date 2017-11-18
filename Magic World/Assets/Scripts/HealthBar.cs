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
		MaxHealthAlwaysPositive();
	}

	void OnGUI(){
        HealthBarGUI();
	}


	private void MaxHealthAlwaysPositive(){
		//makes sure the maximum health is always at least 1
		if(maxHealth < 1){
			maxHealth = 1;
		}
	}

    public void SetMaxHealth(int newMaxHealth) {
        maxHealth = newMaxHealth;
    }

    public void TakeDamage(int damage) {
        curHealth -= damage;

        if (curHealth < 0) {
            curHealth = 0;
        }
    }

    public void HealHealth(int healing) {
        curHealth += healing;

        if (curHealth > maxHealth) {
            curHealth = maxHealth;
        }
    }

    public bool IsDead() {
        return curHealth == 0;
    }


    public void HealthBarGUI() {
        float healthbarWidth = Screen.width / 2 * (curHealth / (float)maxHealth);

        GUI.Box(new Rect(10, 10, healthbarWidth / 2, 20),
            curHealth + "/" + maxHealth);
    }
}
