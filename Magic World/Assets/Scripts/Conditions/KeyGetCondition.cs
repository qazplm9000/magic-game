using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InputSystem;

[CreateAssetMenu(menuName = "Conditions/Key Get Condition")]
public class KeyGetCondition : Condition
{
    public string key;

    public override bool _Execute(CharacterManager manager)
    {
        return World.inputs.GetKey(key);
    }
}
