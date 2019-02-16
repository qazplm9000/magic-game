using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace StatSystem {


    public abstract class StatCalculation : ScriptableObject
    {
        public abstract int CalculateStat(CharacterStats stats, string statName);
    }
}