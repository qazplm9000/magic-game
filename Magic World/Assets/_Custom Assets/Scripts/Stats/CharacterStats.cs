using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpellSystem;
using UnityEngine.UI;

namespace StatSystem
{
    public class CharacterStats : MonoBehaviour
    {

        public Stat currentHealth;
        public Stat maxHealth;
        public Stat currentMana;
        public Stat maxMana;
        public Stat strength;
        public Stat defense;
        public Stat magic;
        public Stat magicDefense;
        public Stat agility;
        public Stat attackTime;

        //stat multipliers go here
        public Stat healthMultiplier;


        public delegate void HealthUpdate();
        public event HealthUpdate healthUpdate;


        public bool isDead = false;

        public delegate void CharacterDeath();
        public event CharacterDeath OnDeath;

        private void Awake()
        {
            currentHealth.Init();
            maxHealth.Init();
            currentMana.Init();
            maxMana.Init();
            strength.Init();
            magic.Init();
            defense.Init();
            magicDefense.Init();
            agility.Init();
            attackTime.Init();
        }


        /// <summary>
        /// Character takes damage
        /// Also checks if character has died
        /// </summary>
        /// <param name="spell"></param>
        /// <param name="enemyStats"></param>
        public void TakeDamage(int damage)
        {
            currentHealth.RemoveStat(damage);
            CheckIsDead();
        }

        /// <summary>
        /// Character has health healed
        /// </summary>
        /// <param name="healAmount"></param>
        public void HealDamage(int healAmount) {
            currentHealth.AddStat(healAmount);
            if (currentHealth.value > maxHealth.value) {
                currentHealth.value = maxHealth.value;
            }
        }

        /// <summary>
        /// Returns true if the target has 0 HP.
        /// Calls OnDeath event when character dies.
        /// </summary>
        /// <returns></returns>
        public void CheckIsDead() {
            if (currentHealth.value == 0 && !isDead) {
                isDead = true;

                if (OnDeath != null) {
                    OnDeath();
                }
            }
        }

        
        

    }
}