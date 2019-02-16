using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StatSystem
{
    [System.Serializable]
    public class Stat
    {
        public string statName;
        public StatCalculation statCalculation;
        public int value;

        public Stat(Stat newStat, int newValue) {
            statName = newStat.statName;
            statCalculation = newStat.statCalculation;
            value = newValue;
        }
    }
}