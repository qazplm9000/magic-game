using EffectSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SkillSystem
{
    public abstract class Skill : ScriptableObject, ISerializationCallbackReceiver
    {
        public string skillName;
        public SkillTargetType targetType;
        public int potency;
        public Element element;
        public List<SkillAnimation> animations = new List<SkillAnimation>();
        public List<SkillObjectCreation> skillObjects = new List<SkillObjectCreation>();
        [Tooltip("List of effects to apply")]
        public List<Effect> effects = new List<Effect>();
        [Range(0,5)]
        public float castTime;
        public int animationType;

        public List<SkillAnimation> GetAnimations() { return animations; }
        public SkillTargetType GetTargetType() { return targetType; }
        public float GetCastTime() { return castTime; }
        


        public void RunSkill(SkillCastData data)
        {
            data.Tick();

            if (data.AtTime(0))
            {
                OnStart(data);
            }

            OnRun(data);

            RunAnimations(data);
            CreateSkillObjects(data);

            if (data.AtTime(castTime))
            {
                OnFinish(data);
            }

        }
        
        public bool IsFinished(SkillCastData data)
        {
            return data.PastTime(castTime);
        }

        private void RunAnimations(SkillCastData data)
        {
            for (int i = 0; i < animations.Count; i++)
            {
                animations[i].RunAnimation(data);
            }
        }

        private void CreateSkillObjects(SkillCastData data)
        {
            for(int i = 0; i < skillObjects.Count; i++)
            {
                skillObjects[i].CreateObject(data);
            }
        }


        protected abstract void OnStart(SkillCastData data);
        protected abstract void OnRun(SkillCastData data);
        protected abstract void OnFinish(SkillCastData data);



        /*
            Serialization
             */

        public void OnBeforeSerialize()
        {
            if (Application.isEditor)
            {
                for (int i = 0; i < effects.Count; i++)
                {
                    effects[i].UpdateDescription();
                }
            }
        }

        public void OnAfterDeserialize()
        {
            
        }
    }
}