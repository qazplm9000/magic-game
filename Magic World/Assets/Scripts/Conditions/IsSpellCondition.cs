using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AbilitySystem;

[CreateAssetMenu(menuName = "Conditions/Is Spell Condition")]
public class IsSpellCondition : Condition
{
    public override bool _Execute(CharacterManager character)
    {
        bool result = false;

        if (character.currentAbility != null) {
            result = character.currentAbility.GetType() == typeof(Ability);
        }

        return result;
    }
}
