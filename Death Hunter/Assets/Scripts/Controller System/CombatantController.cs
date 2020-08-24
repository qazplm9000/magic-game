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
        public float attackDistance = 2;
        public float timeSinceLastAttack = 0;


        public CharacterAI ai;
        

        private void Start()
        {
            character = transform.GetComponent<Combatant>();
        }

        private void Update()
        {
            timeSinceLastAttack += Time.deltaTime;
        }

        public void ControlCharacter() {
            if (ai != null) {
                ai.ControlCharacter(this, character);
            }
        }

        public void ResetTimeSinceLastAttack()
        {
            timeSinceLastAttack = 0;
        }
        
        
    }
}