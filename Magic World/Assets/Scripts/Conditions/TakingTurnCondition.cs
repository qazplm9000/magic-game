using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Conditions/Taking Turn")]
public class TakingTurnCondition : Condition
{
    public override bool _Execute(CharacterManager character)
    {
        return World.battle.currentTurn == character;
    }
    
}
