using CombatSystem.SkillSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace CombatSystem.AI
{
    public class CombatantController : MonoBehaviour
    {

        public TakeTurnBehaviour turnBehaviour;
        public IdleBehaviour idleBehaviour;
        public DefendBehaviour defendBehaviour;
        
        public Combatant character;
        public Skill currentSkill = null;
        public Combatant currentTarget = null;
        

        private void Start()
        {
            character = transform.GetComponent<Combatant>();
        }

        public void TakeTurn(CombatManager battle) {
            if (turnBehaviour != null)
            {
                turnBehaviour.TakeTurn(battle, this, character);
            }
        }

        public void Idle(CombatManager battle) {
            //Consider allowing certain monsters to move around
            if(idleBehaviour != null)
            {
                idleBehaviour.Idle(battle, this, character);
            }
        }

        public void Defend(CombatManager battle) {
            if (defendBehaviour != null) {
                defendBehaviour.Defend(battle, this, character);
            }
        }

    }
}