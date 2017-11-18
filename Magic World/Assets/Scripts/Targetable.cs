using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Targetable : MonoBehaviour {

    public int currentHealth = 100;
    public int maxHealth = 100;

    public int currentMana = 100;
    public int maxMana = 100;

    public bool dead = false;

    private EnemyAI movement;

    private Animator anim;

    public Texture2D portrait;

    public Relation relation = Relation.Enemy;

	// Use this for initialization
	void Start () {
        anim = GetComponentInChildren<Animator>();
        movement = GetComponent<EnemyAI>();
	}


    private void Update()
    {

        CheckIsDead();
        

    }



    public void TakeDamage(int damage) {
        int totalDamage = damage;

        //calculate total damage here

        if (totalDamage <= 0) {
            return;
        }

        currentHealth -= totalDamage;

        if (currentHealth < 0) {
            currentHealth = 0;
        }


    }

    public void ReceiveHealing(int healing) {
        int totalHealing = healing;

        //calculate healing here

        if (healing < 0) {
            healing = 0;
        }

        currentHealth += healing;

        if (currentHealth >= maxHealth) {
            currentHealth = maxHealth;
        }
    }

    public void CheckIsDead() {
        if (!dead && currentHealth == 0)
        {
            dead = true;

            if (anim != null)
            {
                
                //Play death animation
                //anim.Play("Die");

            }
            //deactivate movement script
            if (movement != null)
            {
                movement.enabled = false;
            }
        }
    }

}

public enum Relation {
    Self,
    Friendly,
    Enemy
}