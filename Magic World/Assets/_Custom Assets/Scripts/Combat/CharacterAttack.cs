using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CombatSystem
{
    public abstract class CharacterAttack : ScriptableObject
    {

        public abstract bool Execute(CharacterManager character, object args = null);

    }
}