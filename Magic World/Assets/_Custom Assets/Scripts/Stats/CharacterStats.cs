using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpellSystem;
using UnityEngine.UI;

namespace StatSystem
{
    [System.Serializable]
    public class CharacterStats
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

        public bool isDead = false;


        public void InitStats(List<int> values) {
            
        }


        /// <summary>
        /// Character takes damage
        /// Also checks if character has died
        /// </summary>
        /// <param name="spell"></param>
        /// <param name="enemyStats"></param>
        public void TakeDamage(int damage)
        {
            //currentHealth.RemoveStat(damage);
            CheckIsDead();
        }

        /// <summary>
        /// Character has health healed
        /// </summary>
        /// <param name="healAmount"></param>
        public void HealDamage(int healAmount) {
            //currentHealth.AddStat(healAmount);
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
            }
        }

        

        
    }
}