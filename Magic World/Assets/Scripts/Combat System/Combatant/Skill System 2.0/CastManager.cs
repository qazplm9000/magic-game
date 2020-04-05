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

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void CastSkill(Skill skill) {
            
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
        }

        public void PlayAnimation(string animationName) {
            user.PlayAnimation(animationName);
        }

        public Combatant GetTarget() { return user.GetTarget(); }
    }
}