using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SkillSystem
{
    [CreateAssetMenu(menuName ="Skills/Bolt Spell")]
    public class BoltSpell : Skill
    {

        public override bool StartCast(CharacterManager user, CharacterManager target)
        {
            bool result = true;

            for (int i = 0; i < castConditions.Count; i++) {
                castConditions[i].Execute(user);
            }
            
            return result;
        }

        public override bool CastingSkill(CharacterManager user, CharacterManager target, float previousFrame, float currentFrame, bool interrupted = false)
        {
            bool result = true;

            for (int i = 0; i < castEffects.Count; i++) {
                float startTime = castEffects[i].startTime;
                float length = castEffects[i].length;
                SkillEffect effect = castEffects[i].effect;
                SkillEffectData data = castEffects[i].data;

                //makes sure the start time is in the current range
                bool inRange = previousFrame <= startTime && currentFrame > startTime;
                if (!inRange)
                {
                    continue;
                }
                else if (currentFrame < startTime) {
                    break;
                }


                if (effect != null)
                {
                    result = effect.Execute(user, target, data);
                }
                else {
                    result = false;
                }

                if (!result) {
                    break;
                }
            }

            return result;
        }

        public override bool UsingSkill(CharacterManager user, CharacterManager target, float previousFrame, float currentFrame, bool interrupted = false)
        {
            bool result = true;

            for (int i = 0; i < useEffects.Count; i++) {
                float startTime = useEffects[i].startTime;
                float length = useEffects[i].length;
                SkillEffect effect = useEffects[i].effect;
                SkillEffectData data = useEffects[i].data;

                //makes sure the start time is in the current range
                bool inRange = previousFrame <= startTime && currentFrame > startTime;
                if (!inRange)
                {
                    continue;
                }
                else if (currentFrame < startTime)
                {
                    break;
                }

                if (effect != null)
                {
                    Debug.Log("Called effect");
                    result = effect.Execute(user, target, data);
                }
                else
                {
                    result = false;
                }

                if (!result)
                {
                    break;
                }
            }

            return result;
        }
    }
}