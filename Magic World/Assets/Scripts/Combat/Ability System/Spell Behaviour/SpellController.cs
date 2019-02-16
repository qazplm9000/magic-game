using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace AbilitySystem
{
    public abstract class SpellController : ScriptableObject
    {

        public abstract void Execute(SpellBehaviour spell);


    }
}