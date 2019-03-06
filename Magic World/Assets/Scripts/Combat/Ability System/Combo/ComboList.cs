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


        /// <summary>
        /// Returns the combo at the specified index
        /// Automatically applies modulo
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public Combo GetComboAtIndex(int index) {
            return combos[index % combos.Count];
        }
    }
}