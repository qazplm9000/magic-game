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
        public List<SkillCasterAnimation> casterAnimations;
        public List<SkillObjectData> skillObjects;
        [Range(0,5)]
        public float castTime;

        

        public List<SkillObjectData> GetSkillObjects() { return skillObjects; }
        public List<SkillCasterAnimation> GetCasterAnimations() { return casterAnimations; }
        public SkillTargetType GetTargetType() { return targetType; }


        public void OnBeforeSerialize()
        {
            float highest = 0;

            for (int i = 0; i < casterAnimations.Count; i++) {
                highest = Mathf.Max(casterAnimations[i].startTime, highest);
            }

            for (int i = 0; i < skillObjects.Count; i++) {
                highest = Mathf.Max(skillObjects[i].startTime, highest);
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