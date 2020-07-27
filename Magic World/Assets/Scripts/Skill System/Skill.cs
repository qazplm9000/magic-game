﻿using EffectSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SkillSystem
{
    [CreateAssetMenu(menuName = "Skill/Skill", fileName = "Skill")]
    public class Skill : ScriptableObject, ISerializationCallbackReceiver
    {
        public string skillName;
        public SkillTargetType targetType;
        public List<bool> animationsFoldouts;
        public List<SkillAnimation> animations;
        public List<GameObject> skillObjects;
        public List<Effect> effects = new List<Effect>();
        [Range(0,5)]
        public float castTime;
        public int animationType;

        public virtual List<SkillAnimation> GetAnimations() { return animations; }
        public GameObject GetGameObject(int index) { return skillObjects[index]; }
        public SkillTargetType GetTargetType() { return targetType; }
        public virtual float GetCastTime() { return castTime; }
        

        public Effect GetSkillEffect(int index) {
            return effects[index];
        }


        public void OnBeforeSerialize()
        {
            float highest = 0;

            for (int i = 0; i < animations.Count; i++) {
                highest = Mathf.Max(animations[i].startTime, highest);
            }

            if (castTime < highest) {
                castTime = highest;
            }
        }

        public void OnAfterDeserialize()
        {
            
        }
    }
}