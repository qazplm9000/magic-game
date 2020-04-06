using CombatSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace NewSkillSystem
{
    public class CastManager : MonoBehaviour
    {

        public Combatant user;
        public Skill currentSkill;

        public Combatant target;

        private float previousFrame = 0;
        private float currentFrame = 0;

        private bool isCasting = false;

        private List<GameObject> spellObjects;
        public int maxSpellObjects = 10;

        // Start is called before the first frame update
        void Start()
        {
            InitSpellobjects();
        }

        // Update is called once per frame
        void Update()
        {
            if (IsCasting()) {
                currentSkill.CastSkill(this, previousFrame, currentFrame);
                Tick();
            }
        }

        public void CastSkill(Skill skill) {
            if (currentSkill == null) {
                currentSkill = skill;
                isCasting = true;
            }
        }

        
        public bool IsCasting() { return isCasting; }

        private void Tick() {
            previousFrame = currentFrame;
            currentFrame += Time.deltaTime;
        }

        private void ResetCast()
        {
            previousFrame = 0;
            currentFrame = 0;
            isCasting = false;
            currentSkill = null;
        }

        private void InitSpellobjects() {
            spellObjects = new List<GameObject>(10);
            for (int i = 0; i < maxSpellObjects; i++)
            {
                spellObjects.Add(null);
            }
        }

        public void PlayAnimation(string animationName) {
            user.PlayAnimation(animationName);
        }

        public Combatant GetTarget() { return user.GetTarget(); }

        public void CreateSpellObject(GameObject obj, int index) {
            if (spellObjects.Count > index && index >= 0) {
                spellObjects[index] = obj;
            }
        }

        public GameObject GetSpellObject(GameObject obj, int index) {
            GameObject result = null;

            if (spellObjects.Count > index && index >= 0) {
                result = spellObjects[index];
            }

            return result;
        }
    }
}