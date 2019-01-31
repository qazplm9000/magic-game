using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace TargettingSystem
{
    public abstract class Targetter : ScriptableObject
    {

        public abstract CharacterManager GetTarget(CharacterManager character, object args = null);

    }
}