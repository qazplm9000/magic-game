using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Targetable : MonoBehaviour {

    public int currentHealth = 100;
    public int maxHealth = 100;

    public int currentMana = 100;
    public int maxMana = 100;

    public float mps = 1;

    public bool dead = false;

    private EnemyAI movement;

    private Animator anim;

    public Texture2D portrait;

    public Relation relation = Relation.Enemy;

    private bool regainingMP;
    private bool regainingHealth;

	// Use this for initialization
	void Start () {
        anim = GetComponentInChildren<Animator>();
        movement = GetComponent<EnemyAI>();
	}


    private void OnEnable()
    {
        currentHealth = maxHealth;
        currentMana = maxMana;
        dead = false;

        StartCoroutine(RegainMP());
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

        if (currentHealth < 0)
        {
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

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

    //Returns falst if not enough mana
    public bool UseMana(int mana) {
        bool result = true;

        if (mana <= currentMana)
        {
            currentMana -= mana;
        }
        else {
            result = false;
        }

        if (currentMana < 0)
        {
            currentMana = 0;
        }
        else if (currentMana > maxMana)
        {
            currentMana = maxMana;
        }

        return result;
    }

    


    public void CheckIsDead() {
        if (!dead && currentHealth <= 0)
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
                movement.dead = true;
            }
        }
    }

    public IEnumerator RegainMP()
    {
        float tempMP = 0;

        while (!regainingMP) {
            regainingMP = true;
            tempMP += mps * Time.deltaTime;

            currentMana += (int)tempMP;
            tempMP -= (int)tempMP;
            currentMana = Mathf.Min(currentMana, maxMana);

            yield return null;
            regainingMP = false;
        }

    }

}

public enum Relation {
    Self,
    Friendly,
    Enemy
}