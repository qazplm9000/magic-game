using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace CombatSystem.AI
{
    public abstract class IdleBehaviour : ScriptableObject
    {
        public abstract void Idle(CombatManager battle, CombatantController controller, Combatant character);
    }
}