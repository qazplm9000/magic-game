using CombatSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SkillSystem
{

    public class SkillCastData
    {
        public Combatant caster;
        public Combatant target;
        public Skill skill;
        public Timer timer;

        public SkillCastData(Combatant caster, Combatant target, Skill skill)
        {
            this.caster = caster;
            this.target = target;
            this.skill = skill;
            timer = new Timer();
        }

        public SkillCastData(SkillCastData data, Combatant target)
        {
            caster = data.caster;
            this.target = target;
            skill = data.skill;
            timer = new Timer();
        }

        public void Tick()
        {
            timer.Tick();
        }

        public bool AtTime(float time)
        {
            return timer.AtTime(time);
        }

        public bool PastTime(float time)
        {
            return timer.PastTime(time);
        }
    }


    public class CastManager : MonoBehaviour
    {

        public Combatant caster;

        public Skill currentSkill;
        public SkillCastData castData;

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
                currentSkill.RunSkill(castData);
                caster.SetAnimationBool("isCasting", true);

                if (currentSkill.IsFinished(castData))
                {
                    caster.ChangeFlag(StateSystem.Flag.character_is_casting, false);
                    ResetCast();
                    caster.SetAnimationBool("isCasting", false);
                }
            }
        }

        public void CastSkill(Skill skill) {
            if (currentSkill == null) {
                currentSkill = skill;
                Combatant target = GetProperTarget(skill.GetTargetType());
                castData = new SkillCastData(caster, target, skill);

                caster.ChangeFlag(StateSystem.Flag.character_is_casting, true);
            }
        }


        public bool IsCasting() { return currentSkill != null; }
        

        private void ResetCast()
        {
            currentSkill = null;
            castData = null;
        }






        /// <summary>
        /// Saves the current target to cast at
        /// </summary>
        private Combatant GetProperTarget(SkillTargetType targetType) {
            Combatant target = null;
            switch (targetType)
            {
                case SkillTargetType.Enemy:
                    target = caster.GetTarget();
                    break;
                case SkillTargetType.Ally:
                    target = caster;
                    break;
                case SkillTargetType.Self:
                    target = caster;
                    break;
                case SkillTargetType.AllyParty:
                    break;
                case SkillTargetType.EnemyParty:
                    break;
            }

            return target;
        }

    }
}