using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Conditions/Turn Time")]
public class TurnTimeCondition : Condition
{
    public override bool _Execute(CharacterManager character)
    {
        return World.battle.turnTimer > 0;
    }
    
}
