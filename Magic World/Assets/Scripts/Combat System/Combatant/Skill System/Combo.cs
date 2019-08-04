using CombatSystem.SkillSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace CombatSystem
{
    [System.Serializable]
    public class Combo
    {

        public List<Skill> combos;
        private int nextIndex = 0;

        public Combo(List<Skill> newCombos){
            combos = newCombos;
        }

        /// <summary>
        /// Gets the next combo in the list and increments the combo
        /// </summary>
        /// <returns></returns>
        public Skill GetNextCombo() {
            Skill result = null;

            if (combos != null && combos.Count > 0)
            {
                result = combos[nextIndex % combos.Count];
                nextIndex++;
            }

            return result;
        }

        public void ResetCombo() {
            nextIndex = 0;
        }

    }
}