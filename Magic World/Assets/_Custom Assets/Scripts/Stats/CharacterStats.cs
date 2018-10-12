using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpellSystem;
using UnityEngine.UI;

namespace StatSystem
{
    public class CharacterStats : MonoBehaviour
    {

        public SlidingStat health;
        public SlidingStat mana;
        public Stat strength;
        public Stat magic;
        public Stat defense;

        public Slider healthBar;

        public bool isDead = false;

        public delegate void CharacterDeath();
        public event CharacterDeath OnDeath;

        private void Start()
        {
            health.Init();
            mana.Init();
            strength.Init();
            magic.Init();
            defense.Init();
            UpdateHealth();
        }


        /// <summary>
        /// Character takes damage
        /// Also checks if character has died
        /// </summary>
        /// <param name="spell"></param>
        /// <param name="enemyStats"></param>
        public void TakeDamage(int damage)
        {
            health.ReduceValue(damage);
            CheckIsDead();
            UpdateHealth();
        }

        /// <summary>
        /// Character has health healed
        /// </summary>
        /// <param name="healAmount"></param>
        public void HealDamage(int healAmount) {
            health.IncreaseValue(healAmount);
            UpdateHealth();
        }

        /// <summary>
        /// Returns true if the target has 0 HP.
        /// Calls OnDeath event when character dies.
        /// </summary>
        /// <returns></returns>
        public void CheckIsDead() {
            if (health.currentValue == 0 && !isDead) {
                isDead = true;

                if (OnDeath != null) {
                    OnDeath();
                }
            }
        }


        /// <summary>
        /// Updates the healthbar
        /// </summary>
        public void UpdateHealth() {
            if (healthBar != null)
            {
                healthBar.value = health.currentValue / (float)health.totalValue;
            }
        }
        

    }
}