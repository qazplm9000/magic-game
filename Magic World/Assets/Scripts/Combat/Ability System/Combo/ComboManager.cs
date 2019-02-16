using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AbilitySystem;

namespace AbilitySystem
{
    public class ComboManager : MonoBehaviour
    {
        private CharacterManager character;
        public ComboList combo;
        public Combo currentCombo;
        private int currentComboCount = 0;

        private float previousFrame = 0;
        private float currentFrame = 0;

        private void Start()
        {
            character = transform.GetComponent<CharacterManager>();
            ResetCombo();
        }

        public void ChangeCombo(ComboList newCombos) {
            combo = newCombos;
            ResetCombo();
        }

        public Combo GetNextCombo() {
            Combo nextCombo = combo.combos[currentComboCount];
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
            bool playing = currentCombo.UseAbility(character);

            if (!playing) {
                currentCombo = GetNextCombo();
                ResetTimer();
            }

            return playing;
        }

        /// <summary>
        /// Resets the combo
        /// Call this when exiting attack state
        /// </summary>
        public void ResetCombo() {
            currentComboCount = 0;
            currentCombo = combo.combos[0];
        }

        public void ResetTimer() {
            previousFrame = 0;
            currentFrame = 0;
        }
        
    }
}