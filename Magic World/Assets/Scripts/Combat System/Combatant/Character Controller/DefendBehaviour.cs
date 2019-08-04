using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace CombatSystem.AI
{
    public abstract class DefendBehaviour : ScriptableObject
    {
        public abstract void Defend(CombatManager battle, CombatantController controller, Combatant character);
    }
}