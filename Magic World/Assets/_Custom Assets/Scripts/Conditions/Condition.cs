using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Condition : ScriptableObject {
    public string description;
    public bool reverse;

    public abstract bool _Execute(CharacterManager character);
    public bool Execute(CharacterManager character) {
        bool result = _Execute(character);
        return reverse ? !result : result;
    }

}
