using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AbilitySystem;

namespace ComboSystem
{
    public class ComboManager : MonoBehaviour
    {
        public ComboList combo;
        private int currentCombo = 0;

        public void ChangeCombo(ComboList newCombos) {
            combo = newCombos;
            currentCombo = 0;
        }

        public Ability GetNextCombo() {
            Ability nextCombo = combo.combos[currentCombo];
            currentCombo = (currentCombo + 1) % combo.combos.Count;
            return nextCombo;
        }

        
    }
}