using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AbilitySystem
{
    [CreateAssetMenu(menuName = "Combos/Combo")]
    public class Combo : ScriptableObject
    {
        new public string name;
        public string animation;
        public List<SimpleCaster.Ability> spells;
    }
}