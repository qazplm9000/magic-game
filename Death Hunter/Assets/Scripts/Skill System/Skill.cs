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
        public List<bool> animationsFoldouts;
        public List<SkillAnimation> animations = new List<SkillAnimation>();
        public List<GameObject> skillObjects;
        public List<SkillEffect> effects = new List<SkillEffect>();
        [Range(0,5)]
        public float castTime;
        public int animationType;

        public virtual List<SkillAnimation> GetAnimations() { return animations; }
        public GameObject GetGameObject(int index) { return skillObjects[index]; }
        public SkillTargetType GetTargetType() { return targetType; }
        public virtual float GetCastTime() { return castTime; }
        


        public void RunSkill(SkillCastData data)
        {
            data.Tick();
            RunAnimations(data);
            RunEffects(data);
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

        private void RunEffects(SkillCastData data)
        {
            for(int i = 0; i < effects.Count; i++)
            {
                effects[i].RunEffect(data);
            }
        }






        /*
            Serialization
             */

        public void OnBeforeSerialize()
        {
            if (Application.isEditor)
            {
                for (int i = 0; i < animations.Count; i++)
                {
                    animations[i].UpdateDescription();
                }
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