using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace StatSystem {
    [System.Serializable]
    public class Stat {

        public StatType statType;
        public int initialValue = 10;
        public float levelMultiplier = 5f;
        public int baseValue = 10;


        //temporary and permanent stat boosts
        private List<int> statModifiers;
        public int totalModifier = 0;

        //temporary and permanent stat multipliers
        private List<float> statMultipliers;
        public float totalMultiplier = 1;

        public delegate void TotalValueChanged();
        public event TotalValueChanged OnTotalValueChanged;

        public int totalValue;

        public Stat() {
            Init();
            CalculateTotalValue();
        }

        public Stat(int newValue) {
            initialValue = newValue;
            Init();
        }

        #region stat modifiers
        //adds a stat modifier in the list
        public void AddStatModifier(int modifier) {
            statModifiers.Add(modifier);
        }

        //removes a stat modifier in the list if it exists
        public bool RemoveStatModifier(int modifier) {
            bool result = false;

            if (statModifiers.Contains(modifier))
            {
                statModifiers.Remove(modifier);
                result = true;
            }

            return result;
        }

        //calculates total value of modifier bonuses
        protected void CalculateTotalModifier() {
            int tempModifier = 0;

            for (int i = 0; i < statModifiers.Count; i++) {
                totalModifier += statModifiers[i];
            }

            totalModifier = tempModifier;
        }
        #endregion

        #region multipliers
        //adds a multiplier
        public void AddMultiplier(float multiplier) {
            if (multiplier >= 0)
            {
                statMultipliers.Add(multiplier);
            }
        }

        //removes a multiplier and returns true if it exists
        public bool RemoveMultiplier(float multiplier) {
            bool result = false;

            if (statMultipliers.Contains(multiplier)) {
                statMultipliers.Remove(multiplier);
                result = true;
            }

            return result;
        }

        protected void CalculateTotalMultipliers() {
            float temp = 1;

            for (int i = 0; i < statMultipliers.Count; i++) {
                temp *= statMultipliers[i];
            }

            totalMultiplier = temp;
        }

        protected int CalculateStatFromLevels() {
            int result = 0;

            return result;
        }
        #endregion

        //calculates what the total value of the stat is after boosts
        public void CalculateTotalValue() {
            totalValue = (int)((baseValue + totalModifier) * totalMultiplier);

            if (OnTotalValueChanged != null) {
                OnTotalValueChanged();
            }

        }



        public virtual void Init() {
            statModifiers = new List<int>();
            statMultipliers = new List<float>();
            baseValue = initialValue + CalculateStatFromLevels();
            CalculateTotalValue();
        }
    }
}