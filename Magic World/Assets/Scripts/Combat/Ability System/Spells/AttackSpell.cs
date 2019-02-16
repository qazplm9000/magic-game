using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AbilitySystem
{
    [CreateAssetMenu(menuName = "Abilities/Spells/Attack")]
    public class AttackSpell : Ability
    {

        protected override bool Execute(CharacterManager caster, float previousFrame, float currentFrame) {
            return caster.Attack();
        }

    }
}