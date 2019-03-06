using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpellSystem;
using UnityEngine.UI;
using AbilitySystem;

namespace StatSystem
{
    [System.Serializable]
    public class CharacterStats : MonoBehaviour
    {
        private CharacterManager character;


        public int currentHealth;
        public int maxHealth;
        public int staggerThreshold = 100;
        public int baseStaggerThreshold = 100;
        public int staggerAmount = 0;
        public int staggerLevel = 0;
        [Range(0, 1)]
        public float staggerResistance = 0;
        public int strength;
        public int defense;
        public int magic;
        public int magicDefense;
        public int agility;
        public float attackTime;
        public float incompatibleElementBonus;
        public float compatibleElementBonus;
        public float sameElementBonus;
        public AbilityElement element;

        //stat multipliers go here
        public float healthMultiplier;

        public bool isDead = false;
        public bool hasPrecedence = false;


        private void Start()
        {
            character = transform.GetComponent<CharacterManager>();
        }



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





        /***************************************/
        /***************************************/
        /************ Staggering ***************/
        /***************************************/
        /***************************************/

        /// <summary>
        /// Increases stagger amount
        /// </summary>
        public void StaggerDamage(int stagger) {
            staggerAmount += stagger;

            if (staggerAmount > staggerThreshold) {
                StaggerLevelUp();
            }
        }


        /// <summary>
        /// Raises the stagger level
        /// Resets the stagger amount
        /// Raises the stagger threshold
        /// </summary>
        public void StaggerLevelUp() {
            staggerLevel++;
            //<Include logic for reducing stagger resistance here if needed>
            staggerAmount = 0;
            staggerThreshold = (int)(staggerThreshold * 1.2);
        }



        /// <summary>
        /// Completely resets the current stagger
        /// </summary>
        public void StaggerReset() {
            staggerAmount = 0;
            staggerThreshold = baseStaggerThreshold;
            staggerLevel = 0;
        }









        /// <summary>
        /// Returns true if the target has 0 HP.
        /// </summary>
        /// <returns></returns>
        public bool IsDead() {
            return currentHealth <= 0;
        }


        /// <summary>
        /// Returns true the frame the character dies in
        /// </summary>
        /// <returns></returns>
        public bool HasDied() {
            bool hasDied = false;

            if (!isDead && currentHealth <= 0) {
                hasDied = true;
                isDead = true;
                character.RaiseEvent("OnCharacterDeath");
            }

            return hasDied;
        }


        public AbilityElement GetElement() {
            return element;
        }








        /// <summary>
        /// Calculates how long for the character's next turn to start
        /// </summary>
        /// <param name="previousTurn"></param>
        /// <returns></returns>
        public float CalculateNextTurn(float previousTurn) {
            return previousTurn + (1000f / (agility + 100));
        }

        /// <summary>
        /// Calculates how long for the character's first turn
        /// </summary>
        /// <param name="previousTurn"></param>
        /// <returns></returns>
        public float CalculateFirstTurn() {
            return hasPrecedence ? 0 : CalculateNextTurn(0);
        }
        
    }
}