using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace CombatSystem.AI
{
    public abstract class CharacterAI : ScriptableObject
    {
        public abstract void ControlCharacter(CombatantController controller, Combatant combatant);
    }
}