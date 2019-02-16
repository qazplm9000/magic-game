using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StatSystem
{
    public abstract class DamageFormula : ScriptableObject
    {

        public abstract int CalculateDamage();
        
    }
}