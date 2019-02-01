using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace StatSystem {

    [CreateAssetMenu(menuName = "")]
    public class Stat : ScriptableObject
    {
        public int value;

        public virtual int CalculateStat(CharacterStats stats) {
            return value;
        }

        public void AddStat(int addition) {
            value += addition;
        }


    }
}