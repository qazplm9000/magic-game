using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Conditions/State Time Condition")]
public class StateTimeCondition : Condition
{
    public float threshold;

    public override bool _Execute(CharacterManager character)
    {
        return character.GetStateTime() >= threshold;
    }
    
}
