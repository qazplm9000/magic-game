using CombatSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace NewSkillSystem
{
    [System.Serializable]
    public class SkillAnimation
    {
        public SkillAnimationType animationType;
        public float startTime;

        public string animationName;
        public float speed = 5;

        /// <summary>
        /// Apply an animation directly on the caster
        /// </summary>
        /// <param name="caster"></param>
        /// <param name="previousFrame"></param>
        /// <param name="currentFrame"></param>
        public void PlayAnimation(CastManager caster, float previousFrame, float currentFrame) {
            switch (animationType)
            {
                case SkillAnimationType.PlayAnimation:
                    if(startTime <= currentFrame)
                    caster.PlayAnimation(animationName);
                    break;
                case SkillAnimationType.MoveTowardsTarget:
                    break;
            }
        }

        /// <summary>
        /// Apply an animation on the object
        /// </summary>
        /// <param name="caster"></param>
        /// <param name="obj"></param>
        /// <param name="previousFrame"></param>
        /// <param name="currentFrame"></param>
        public void PlayAnimation(CastManager caster, GameObject obj, float previousFrame, float currentFrame) {
            switch (animationType)
            {
                case SkillAnimationType.PlayAnimation:
                    break;
                case SkillAnimationType.MoveTowardsTarget:
                    Combatant target = caster.GetTarget();
                    obj.transform.LookAt(target.transform);
                    obj.transform.position += obj.transform.forward * speed * Time.deltaTime;
                    break;
            }


        }

        private bool AnimationStarted(float previousFrame, float currentFrame) {
            bool result = false;

            if (startTime == 0 && currentFrame == 0)
            {
                result = true;
            }
            else if (startTime <= currentFrame && startTime >= previousFrame) {
                result = true;
            }

            return result;
        }
    }
}