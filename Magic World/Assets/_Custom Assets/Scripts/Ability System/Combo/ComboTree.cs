using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AbilitySystem
{
    [CreateAssetMenu(menuName = "Combos/Combo Tree")]
    public class ComboTree : ScriptableObject
    {

        public List<Ability> lightCombos;
        public List<Ability> heavyCombos;

    }
}