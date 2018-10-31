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

        public delegate void HealthUpdate();
        public event HealthUpdate healthUpdate;


        public bool isDead = false;

        public delegate void CharacterDeath();
        public event CharacterDeath OnDeath;

        private void Awake()
        {
            health.Init();
            mana.Init();
            strength.Init();
            magic.Init();
            defense.Init();
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
        }

        /// <summary>
        /// Character has health healed
        /// </summary>
        /// <param name="healAmount"></param>
        public void HealDamage(int healAmount) {
            health.IncreaseValue(healAmount);
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

        
        

    }
}