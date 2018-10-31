using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StatSystem
{
    [System.Serializable]
    public class SlidingStat : Stat
    {
        //[HideInInspector]
        public int currentValue {
            get { return _currentValue; }
            set {
                if (valueUpdate != null) {
                    valueUpdate();
                }
                _currentValue = value;
            }
        }

        [SerializeField]
        private int _currentValue;

        public delegate void ValueUpdate();
        public event ValueUpdate valueUpdate;

        public SlidingStat() : base() {
            currentValue = totalValue;
        }

        /// <summary>
        /// Returns true if the amount is greater than the current value
        /// </summary>
        /// <param name="amount"></param>
        /// <returns></returns>
        public bool HasEnough(int amount) {
            bool result = false;

            if (amount <= currentValue) {
                result = true;
            }

            return result;
        }

        /// <summary>
        /// Reduces the current value by amount. Does not go under 0. 
        /// Does nothing if amount is negative.
        /// </summary>
        /// <param name="amount"></param>
        public void ReduceValue(int amount) {

            if (amount < 0) {
                return;
            }

            currentValue -= amount;
            currentValue = Mathf.Max(0, currentValue);
        }

        /// <summary>
        /// Increases the current value by amount. Does not go over the totalValue.
        /// Does nothing if amount is negative.
        /// </summary>
        /// <param name="amount"></param>
        public void IncreaseValue(int amount) {

            if (amount < 0) {
                return;
            }

            currentValue += amount;
            currentValue = Mathf.Min(currentValue, totalValue);
        }

        public override void Init()
        {
            base.Init();
            currentValue = totalValue;
        }
    }
}