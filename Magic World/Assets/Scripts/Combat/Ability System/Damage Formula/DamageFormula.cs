using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AbilitySystem
{
    public abstract class DamageFormula : ScriptableObject
    {

        public abstract int CalculateDamage(CharacterManager caster, CharacterManager target, Ability ability);

    }
}