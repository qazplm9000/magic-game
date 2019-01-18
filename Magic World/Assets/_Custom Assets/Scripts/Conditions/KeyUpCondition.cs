using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InputSystem;

[CreateAssetMenu(menuName = "Conditions/Key Up Condition")]
public class KeyUpCondition : Condition {

    public string key;

    public override bool Execute(CharacterManager character)
    {
        return World.inputs.GetKeyUp(key);
    }
    
}
