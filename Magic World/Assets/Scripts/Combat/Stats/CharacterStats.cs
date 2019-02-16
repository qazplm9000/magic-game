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
        [Tooltip("Character's current health")]
        public int currentHealth;
        [Tooltip("Character's maximum health")]
        public int maxHealth;
        [Tooltip("Character's current mana")]
        public int currentMana;
        [Tooltip("Character's maximum mana")]
        public int maxMana;
        [Tooltip("")]
        public int strength;
        [Tooltip("")]
        public int defense;
        [Tooltip("")]
        public int magic;
        [Tooltip("")]
        public int magicDefense;
        [Tooltip("")]
        public int agility;
        [Tooltip("")]
        public int delay;
        [Tooltip("")]
        public float attackTime;

        //stat multipliers go here
        public float healthMultiplier;

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
            currentHealth -= damage;
            currentHealth = Mathf.Max(currentHealth, 0);

            if (IsDead()) {
                isDead = true;
                //Run event for dying
            }

        }

        /// <summary>
        /// Character has health healed
        /// </summary>
        /// <param name="healAmount"></param>
        public void HealDamage(int healAmount) {
            currentHealth += healAmount;
            currentHealth = Mathf.Min(currentHealth, maxHealth);
        }

        /// <summary>
        /// Returns true if the target has 0 HP.
        /// </summary>
        /// <returns></returns>
        public bool IsDead() {
            return currentHealth > 0;
        }

        

        
    }
}