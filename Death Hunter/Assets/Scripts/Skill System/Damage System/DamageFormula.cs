using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public abstract class DamageFormula : ScriptableObject
{
    public string description;

    public abstract int CalculateDamage(AttackData data);
}
