using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AbilitySystem
{
    public abstract class SpellLandEffect : ScriptableObject
    {

        public abstract void Execute(CharacterManager caster, CharacterManager target, Ability ability);

    }
}