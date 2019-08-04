using CombatSystem.CastLocationSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace CombatSystem.SkillSystem
{
    public class SkillCaster : MonoBehaviour
    {
        private Combatant caster;
        private CombatManager battle;
        private Skill currentSkill = null;
        private Combatant target;
        private float currentFrame = 0f;
        private float previousFrame = 0f;
        public List<Skill> skillList;
        public List<GameObject> spellObjects;

        // Start is called before the first frame update
        void Start()
        {
            caster = transform.GetComponent<Combatant>();
            spellObjects = new List<GameObject>(5);
            battle = GameObject.FindObjectOfType<CombatManager>();
        }

        // Update is called once per frame
        void Update()
        {
            if (currentSkill != null) {
                Cast();
            }
        }

        private void Tick() {
            previousFrame = currentFrame;
            currentFrame += Time.deltaTime;
        }

        public void CastSkill(Skill skill, Combatant target) {
            if (currentSkill == null) {
                currentSkill = skill;
            }
        }

        /// <summary>
        /// Casts the current skill
        /// </summary>
        private void Cast() {
            Tick();

            currentSkill.CastSkill(battle, caster, previousFrame, currentFrame);

            if (currentSkill.IsSkillFinished(battle, caster, previousFrame, currentFrame))
            {
                currentSkill = null;
            }
        }


        public List<Skill> GetSkillList(){
            return skillList;
        }

        public bool IsCasting() {
            return currentSkill != null;
        }




        public void AddSpellObject(GameObject obj) {
            spellObjects.Add(obj);
        }

        public void ResetSpellObjects() {
            for (int i = 0; i < spellObjects.Count; i++) {
                battle.RemoveObject(spellObjects[i]);
            }
        }

        
    }
}