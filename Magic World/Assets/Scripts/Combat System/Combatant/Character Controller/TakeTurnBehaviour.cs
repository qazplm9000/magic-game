using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace CombatSystem.AI
{
    public abstract class TakeTurnBehaviour : ScriptableObject
    {
        public abstract void TakeTurn(CombatManager battle, CombatantController controller, Combatant character);
    }
}