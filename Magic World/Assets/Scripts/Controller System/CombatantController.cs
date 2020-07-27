using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SkillSystem;


namespace CombatSystem.AI
{
    public class CombatantController : MonoBehaviour
    {
        public Combatant character;
        public Skill characterSkill;
        public Skill characterCombo;
        public Combatant currentTarget = null;

        public CharacterAI ai;
        

        private void Start()
        {
            character = transform.GetComponent<Combatant>();
        }


        public void ControlCharacter() {
            if (ai != null) {
                ai.ControlCharacter(this, character);
            }
        }

        
        
        
    }
}