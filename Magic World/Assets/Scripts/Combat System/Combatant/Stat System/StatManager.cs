using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;



namespace CombatSystem.StatSystem
{
    public class StatManager : MonoBehaviour, ISerializationCallbackReceiver
    {

        public List<string> statNames;
        public List<int> baseStats;
        public List<float> statMultipliers;
        public List<int> statAdders;

        public void Start()
        {
            
        }

        /// <summary>
        /// Gets the total value of the stat
        /// </summary>
        /// <param name="stat"></param>
        /// <returns></returns>
        public int GetStat(Stat stat) {
            int index = (int)stat;
            return (int)((baseStats[index] + statAdders[index]) * statMultipliers[index]);
        }

        /// <summary>
        /// Gets the base value of the stat
        /// </summary>
        /// <param name="stat"></param>
        /// <returns></returns>
        public int GetBaseStat(Stat stat) {
            return baseStats[(int)stat];
        }

        /// <summary>
        /// Gets the multiplier for the selected stat
        /// </summary>
        /// <param name="stat"></param>
        /// <returns></returns>
        public float GetStatMultiplier(Stat stat) {
            return statMultipliers[(int)stat];
        }

        /// <summary>
        /// Gets the total additional stats for the character
        /// </summary>
        /// <param name="stat"></param>
        /// <returns></returns>
        public int GetStatAdder(Stat stat) {
            return statAdders[(int)stat];
        }

        /// <summary>
        /// Adds to the base value of a stat
        /// </summary>
        /// <param name="stat"></param>
        /// <param name="amount"></param>
        public void AddStat(Stat stat, int amount) {
            baseStats[(int)stat] += amount;
        }

        /// <summary>
        /// Adds to the multipliers for a stat
        /// </summary>
        /// <param name="stat"></param>
        /// <param name="amount"></param>
        public void AddStatMultiplier(Stat stat, float amount) {
            statMultipliers[(int)stat] += amount;
        }

        /// <summary>
        /// Adds to total additions for a stat
        /// </summary>
        /// <param name="stat"></param>
        /// <param name="amount"></param>
        public void AddStatAdder(Stat stat, int amount) {
            statAdders[(int)stat] += amount;
        }

        public void SetBaseStat(Stat stat, int value)
        {
            baseStats[(int)stat] = value;
        }


        //Fixes the size of the lists
        public void OnBeforeSerialize()
        {
            int size = Enum.GetValues(typeof(Stat)).Length;

            if (statNames == null) {
                statNames = new List<string>();
                baseStats = new List<int>();
                statMultipliers = new List<float>();
                statAdders = new List<int>();
            }

            if (statNames.Count != size) {
                SetStatNames();
            }

            if (baseStats.Count != size) {
                while (baseStats.Count > size) {
                    baseStats.Remove(baseStats.Count - 1);
                    statMultipliers.Remove(statMultipliers.Count - 1);
                    statAdders.Remove(statAdders.Count - 1);
                }

                while (baseStats.Count < size) {
                    baseStats.Add(0);
                    statMultipliers.Add(0);
                    statAdders.Add(0);
                }
            }
        }

        public void OnAfterDeserialize()
        {
            
        }

        private void SyncronizeListSize() {
            if (baseStats.Count != statAdders.Count && baseStats.Count != statMultipliers.Count) {

            }
        }

        private void SetStatNames() {
            string[] temp = Enum.GetNames(typeof(Stat));
            statNames = new List<string>();

            for (int i = 0; i < temp.Length; i++) {
                statNames.Add(temp[i]);
            }
        }
    }
}