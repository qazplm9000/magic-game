using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;



namespace CombatSystem.StatSystem
{
    public class StatManager : MonoBehaviour
    {
        public int currentHealth;
        public int maxHealth;
        public int strength;
        public int magic;
        public int agility;



        public void Start()
        {
            
        }

        /// <summary>
        /// Gets the total value of the stat
        /// </summary>
        /// <param name="stat"></param>
        /// <returns></returns>
        public int GetStat(Stat stat) {
            int result = 0;

            switch (stat)
            {
                case Stat.CurrentHealth:
                    result = currentHealth;
                    break;
                case Stat.MaxHealth:
                    result = maxHealth;
                    break;
                case Stat.CurrentMana:
                    break;
                case Stat.MaxMana:
                    break;
                case Stat.Strength:
                    result = strength;
                    break;
                case Stat.Magic:
                    break;
                case Stat.Defense:
                    break;
                case Stat.Speed:
                    break;
            }

            return result;
        }
        
        

        /// <summary>
        /// Adds to the base value of a stat
        /// </summary>
        /// <param name="stat"></param>
        /// <param name="amount"></param>
        public void AddStat(Stat stat, int amount) {
            switch (stat)
            {
                case Stat.CurrentHealth:
                    currentHealth += amount;
                    break;
                case Stat.MaxHealth:
                    maxHealth += amount;
                    break;
                case Stat.CurrentMana:
                    break;
                case Stat.MaxMana:
                    break;
                case Stat.Strength:
                    strength += amount;
                    break;
                case Stat.Magic:
                    magic += amount;
                    break;
                case Stat.Defense:
                    break;
                case Stat.Speed:
                    break;
            }
        }


    }
}