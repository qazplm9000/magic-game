using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NewSkillSystem
{
    public class Skill : ScriptableObject
    {
        public string skillName;
        public List<SkillObject> skillObjects;


        public void CastSkill(CastManager caster, float previousFrame, float currentFrame) {
            for (int i = 0; i < skillObjects.Count; i++) {
                SkillObject obj = skillObjects[i];

                if (obj.IsStarting(previousFrame, currentFrame)) {
                    obj.CreateSkillObject(caster, caster.GetTarget());
                }
            }
        }

        public bool IsFinished(float previousFrame, float currentFrame) {
            bool result = true;

            return result;
        }

        public void StartCast(CastManager caster) {

        }
    }
}