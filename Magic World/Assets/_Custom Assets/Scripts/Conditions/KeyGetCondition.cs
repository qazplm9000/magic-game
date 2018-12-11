using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InputSystem;

[CreateAssetMenu(menuName = "Conditions/Key Get Condition")]
public class KeyGetCondition : Condition
{
    public string key;

    public override bool Execute(CharacterManager manager)
    {
        return InputManager.manager.GetKey(key);
    }
}
