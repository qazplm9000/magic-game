using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Conditions/Axis Down Condition")]
public class AxisGetCondition : Condition
{
    public string axis;

    public override bool _Execute(CharacterManager character)
    {
        return World.inputs.GetAxis(axis) > 0;
    }
}

