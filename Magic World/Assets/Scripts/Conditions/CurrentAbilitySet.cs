using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Conditions/Has Current Ability")]
public class CurrentAbilitySet : Condition
{
    public override bool _Execute(CharacterManager character)
    {
        //Debug.Log(character.currentAbility != null);
        throw new System.Exception("Condition not implemented");
        return false;// character.currentAbility != null;
    }
}
