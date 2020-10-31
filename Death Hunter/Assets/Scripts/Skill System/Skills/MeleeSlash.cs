using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SkillSystem
{
    [CreateAssetMenu(menuName = "Skill/Melee Slash", fileName = "Melee Slash")]
    public class MeleeSlash : Skill, ISerializationCallbackReceiver
    {
        [SerializeField]
        private string animationName;
        [SerializeField]
        [Range(0,5)]
        private float hitboxStartTime;
        [SerializeField]
        [Range(0,5)]
        private float hitboxDuration;
        [SerializeField]
        [Range(0,0.5f)]
        private float crossfadeTime;
        [SerializeField]
        private float skillDuration;

        public override bool IsRunning(SkillCastData data)
        {
            return !data.timer.PastTime(skillDuration);
        }

        protected override void EndSkill(SkillCastData data)
        {
            
        }

        protected override void StartSkill(SkillCastData data)
        {
            Caster caster = data.caster;
            caster.PlayAnimation(animationName, crossfadeTime);
        }

        protected override void UpdateSkill(SkillCastData data)
        {
            if (data.AtTime(hitboxStartTime))
            {
                Debug.Log("Init weapon hitbox");
            }

            if(data.AtTime(hitboxStartTime + hitboxDuration))
            {
                Debug.Log("End weapon hitbox");
            }
        }


        public void OnAfterDeserialize()
        {
            skillDuration = skillDuration < hitboxStartTime + hitboxDuration ?
                                hitboxStartTime + hitboxDuration : skillDuration;
        }

        public void OnBeforeSerialize()
        {
            
        }

    }
}