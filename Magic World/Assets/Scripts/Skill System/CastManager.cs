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

        private List<SkillAnimation> animations;
        public int maxSpellObjects = 10;

        public SkillObject soPrefab;

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

                if (TimeReached(currentSkill.GetCastTime()))
                {
                    caster.ChangeFlag(StateSystem.Flag.character_is_casting, false);
                    ResetCast();
                }
            }
        }

        public void CastSkill(Skill skill) {
            if (currentSkill == null) {
                currentSkill = skill;
                animations = skill.GetAnimations();
                
                isCasting = true;
                caster.ChangeFlag(StateSystem.Flag.character_is_casting, true);
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
            for (int i = 0; i < animations.Count; i++) {
                SkillAnimation anim = animations[i];

                if (TimeReached(anim.startTime))
                {
                    switch (anim.animationType)
                    {
                        case SkillAnimationType.PlayAnimation:
                            string animName = anim.animationName;
                            caster.PlayAnimation(animName);
                            break;
                        case SkillAnimationType.PlaySound:
                            break;
                        case SkillAnimationType.CreateObject:
                            SkillObject so = Instantiate(soPrefab);
                            so.StartSkill(currentSkill, caster, target, anim);
                            break;
                    }
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