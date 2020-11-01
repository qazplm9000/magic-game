using EffectSystem;
using System.Collections;
using System.Collections.Generic;
using TargettingSystem;
using UnityEngine;

namespace SkillSystem
{
    public class SkillCastData
    {
        public Caster caster;
        public ITargettable target;
        public Timer timer;
        public List<Effect> effects;

        public SkillCastData(Caster caster, ITargettable target)
        {
            this.caster = caster;
            this.target = target;
            timer = new Timer();
        }

        public bool AtTime(float time)
        {
            return timer.AtTime(time);
        }

        public bool PastTime(float time)
        {
            return timer.PastTime(time);
        }

        public void Tick()
        {
            timer.Tick();
        }
    }

    public abstract class Skill : ScriptableObject
    {

        public IEnumerator RunSkill(SkillCastData data)
        {
            StartSkill(data);

            while (IsRunning(data))
            {
                yield return null;
                data.Tick();
                UpdateSkill(data);
            }

            EndSkill(data);
        }

        public virtual void InterruptSkill(SkillCastData data)
        {
            
        }


        protected abstract void StartSkill(SkillCastData data);
        protected abstract void UpdateSkill(SkillCastData data);
        protected abstract void EndSkill(SkillCastData data);
        public abstract bool IsRunning(SkillCastData data);
    }
}