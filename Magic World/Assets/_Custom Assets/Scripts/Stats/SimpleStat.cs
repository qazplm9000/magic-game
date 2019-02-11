using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StatSystem
{
    public class SimpleStat : StatCalculation
    {
        public override int CalculateStat(CharacterStats stats, string statName)
        {
            return 1;
        }
    }
}