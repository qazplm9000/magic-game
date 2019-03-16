using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AbilitySystem;

namespace AbilitySystem
{
    [CreateAssetMenu(menuName = "Abilities/Combos/Combo Lists/Basic Combo")]
    public class ComboList : ScriptableObject
    {
        public Sprite image;
        [SerializeField]
        private List<Combo> combos;
        public TargetType targetType;

        private int comboIndex = 0;

        /// <summary>
        /// Returns the next combo in the list
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public Combo GetNextCombo() {
            Combo result = null;

            if (combos.Count > 0) {
                result = combos[comboIndex % combos.Count];
            }

            return result;
        }

        public void ResetCombo() {
            comboIndex = 0;
        }

    }
}