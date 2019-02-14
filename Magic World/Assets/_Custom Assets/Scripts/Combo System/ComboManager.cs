using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AbilitySystem;

namespace ComboSystem
{
    public class ComboManager : MonoBehaviour
    {
        private CharacterManager character;
        public ComboList combo;
        public Ability currentCombo;
        private int currentComboCount = 0;

        private void Start()
        {
            character = transform.GetComponent<CharacterManager>();
            ResetCombo();
        }

        public void ChangeCombo(ComboList newCombos) {
            combo = newCombos;
            ResetCombo();
        }

        public Ability GetNextCombo() {
            Ability nextCombo = combo.combos[currentComboCount];
            currentComboCount = (currentComboCount + 1) % combo.combos.Count;
            return nextCombo;
        }

        /// <summary>
        /// Returns true while combo is playing
        /// Returns false once combo has ended
        /// Automatically increments the combo once the current combo has ended
        /// </summary>
        /// <returns></returns>
        public bool PlayCurrentCombo() {
            bool playing = currentCombo.Execute(character);

            if (!playing) {
                currentCombo = GetNextCombo();
            }

            return playing;
        }

        public void ResetCombo() {
            currentComboCount = 0;
            currentCombo = combo.combos[0];
        }
        
    }
}