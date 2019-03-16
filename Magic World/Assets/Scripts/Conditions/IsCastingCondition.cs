using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Conditions/Is Casting")]
public class IsCastingCondition : Condition
{
    public override bool _Execute(CharacterManager character)
    {
        return character.IsCasting();
    }
    
}
