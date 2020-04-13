using CombatSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SkillSystem
{
    public class CastManager : MonoBehaviour
    {

        public Combatant caster;
        private Skill currentSkill;

        public Combatant target;

        private float previousFrame = 0;
        private float currentFrame = 0;

        private bool isCasting = false;

        private List<SkillObjectData> skillObjectData;
        private List<SkillCasterAnimation> casterAnimations;
        public int maxSpellObjects = 10;


        public Skill testSkill;

        // Start is called before the first frame update
        void Start()
        {
            caster = transform.GetComponent<Combatant>();
        }

        // Update is called once per frame
        void Update()
        {
            if (IsCasting())
            {
                Tick();

                RunCurrentSkill();

                if (TimeReached(currentSkill.castTime))
                {
                    ResetCast();
                }
            }
        }

        public void CastSkill(Skill skill) {
            if (currentSkill == null) {
                currentSkill = skill;
                skillObjectData = skill.GetSkillObjects();
                casterAnimations = skill.GetCasterAnimations();
                isCasting = true;
                SetProperTarget(skill.GetTargetType());
            }
        }


        public bool IsCasting() { return isCasting; }

        /// <summary>
        /// Increments the time
        /// </summary>
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

        /// <summary>
        /// Plays the current skill
        /// </summary>
        private void RunCurrentSkill() {
            bool skillIsDone = true;


            //Loops through and creates all skill objects when they should be instantiated
            for (int i = 0; i < skillObjectData.Count; i++) {
                SkillObjectData data = skillObjectData[i];

                if (TimeReached(data.startTime)) {
                    SkillObject so = data.CreateSkillObject(caster, target);
                    Debug.Log("Remember to have this function choose an appropriate target");
                }

                skillIsDone &= data.HasStarted(previousFrame, currentFrame);
            }

            for (int i = 0; i < casterAnimations.Count; i++) {
                SkillCasterAnimation anim = casterAnimations[i];

                if (TimeReached(anim.startTime)) {
                    caster.PlayAnimation(anim.animationName);
                }
            }
        }






        /// <summary>
        /// Saves the current target to cast at
        /// </summary>
        private void SetProperTarget(SkillTargetType targetType) {
            switch (targetType)
            {
                case SkillTargetType.Enemy:
                    break;
                case SkillTargetType.Ally:
                    break;
                case SkillTargetType.Self:
                    break;
                case SkillTargetType.AllyParty:
                    break;
                case SkillTargetType.EnemyPartn:
                    break;
            }

            target = caster.GetTarget();
        }

        public Combatant GetTarget() {
            return target;
        }

        private bool TimeReached(float startTime) {
            return startTime < currentFrame && startTime >= previousFrame;
        }

    }
}