using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InputSystem;

[CreateAssetMenu(menuName = "Conditions/Key Down Condition")]
public class KeyDownCondition : Condition
{
    public string key;

    public override bool Execute(CharacterManager manager)
    {
        return World.inputs.GetKeyDown(key);
    }
}
