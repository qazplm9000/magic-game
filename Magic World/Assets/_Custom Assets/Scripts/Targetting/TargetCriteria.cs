using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TargettingSystem
{
    public abstract class TargetCriteria : ScriptableObject
    {

        public abstract float GetCriteria(CharacterManager character, CharacterManager target);

    }
}