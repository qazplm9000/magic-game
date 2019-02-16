using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Conditions/Current Action Condition")]
public class CurrentActionCondition : Condition
{
    public CharacterAction action;

    public override bool _Execute(CharacterManager character)
    {
        return character.currentAction == action;
    }
}
