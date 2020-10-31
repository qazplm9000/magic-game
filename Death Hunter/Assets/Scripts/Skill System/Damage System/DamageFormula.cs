using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace SkillSystem
{
    public abstract class DamageFormula : ScriptableObject
    {
        public string description;

        public abstract int CalculateDamage(AttackData data);
    }
}