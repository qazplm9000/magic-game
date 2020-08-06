using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CombatSystem.StatSystem
{
    public enum StatType
    {
        CurrentHealth,
        MaxHealth,
        CurrentMana,
        MaxMana,
        Strength,
        Magic,
        Defense,
        Speed,
    }

    [System.Serializable]
    public class Stat : ISerializationCallbackReceiver
    {
        public string description;
        public StatType type;
        public int baseValue = 1;
        public int adder = 0;
        public float multiplier = 1;
        public int totalValue = 1;

        public int GetStatTotal()
        {
            return totalValue;
        }

        public void AddStat(int amount)
        {
            if (amount > 0)
            {
                adder += amount;
                CalculateTotalStat();
            }
        }

        public void RemoveStat(int amount)
        {
            if(amount > 0)
            {
                adder -= amount;
                CalculateTotalStat();
            }
        }

        public void IncreaseMultiplier(float amount)
        {
            if(amount > 0)
            {
                multiplier *= amount;
                CalculateTotalStat();
            }
        }

        public void DecreaseMultiplier(float amount)
        {
            if(amount > 0)
            {
                multiplier /= amount;
                CalculateTotalStat();
            }
        }

        public void AddBaseStat(int amount)
        {
            if(amount > 0)
            {
                baseValue += amount;
                CalculateTotalStat();
            }
        }

        public void RemoveBaseStat(int amount)
        {
            if (amount > 0)
            {
                baseValue -= amount;
                CalculateTotalStat();
            }
        }

        public void SetBaseStat(int value)
        {
            baseValue = value;
        }

        private void CalculateTotalStat()
        {
            totalValue = (int)((baseValue + adder) * multiplier);
        }

        public Stat Copy()
        {
            Stat result = new Stat();
            result.description = description;
            result.type = type;
            result.baseValue = baseValue;
            result.adder = adder;
            result.multiplier = multiplier;
            result.totalValue = totalValue;

            return result;
        }

        public void OnBeforeSerialize()
        {
            if (Application.isEditor)
            {
                CalculateTotalStat();
                description = $"{type.ToString()}: {totalValue}";
            }
        }

        public void OnAfterDeserialize()
        {
            
        }
    }
}