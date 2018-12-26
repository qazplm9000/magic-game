using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarTarget : MonoBehaviour {

    public HealthBar playerHealthBar;
    public HealthBar enemyHealthBar;
    public CharacterManager playerManager;

	// Use this for initialization
	void Start ()
    {
        SetPlayerBar();
        playerManager.OnNewTarget += ChangeTarget;
        //Debug.Log("Added event");
	}

    public void SetPlayerBar() {
        playerManager.stats.currentHealth.OnTotalValueChanged += playerHealthBar.UpdateHealth;
        playerHealthBar.UpdateHealth();
    }

    public void ChangeTarget(TargetPoint target) {
        //removes the current event if the current target is not null
        if (enemyHealthBar.target != null) {
            enemyHealthBar.target.stats.currentHealth.OnTotalValueChanged -= enemyHealthBar.UpdateHealth;
        }

        //sets the new target and sets the new event in place if needed
        if (target != null)
        {
            //set object active
            enemyHealthBar.gameObject.SetActive(true);
            //update target and health event
            enemyHealthBar.target = target.manager;
            enemyHealthBar.target.stats.currentHealth.OnTotalValueChanged += enemyHealthBar.UpdateHealth;
            //update health
            enemyHealthBar.UpdateHealth();
        }
        else {
            enemyHealthBar.target = null;
            enemyHealthBar.gameObject.SetActive(false);
        }
    }
}
