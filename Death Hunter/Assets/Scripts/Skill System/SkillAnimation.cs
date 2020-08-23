using EffectSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace SkillSystem
{
    public enum SkillAnimationType
    {
        PlayAnimation,
        PlaySound,
        ActivateWeapon,
        DeactivateWeapon
    }

    [System.Serializable]
    public class SkillAnimation : ISerializationCallbackReceiver
    {
        public string description = "";
        public SkillAnimationType animationType;
        public float startTime;

        //Used for Play Animation
        [Tooltip("Name of animation to play")]
        public string animationName;
        public float animationCrossfade;

        //Used for Play Sound
        [Tooltip("Sound to play")]
        public AudioClip clip;

        public List<Effect> effects = new List<Effect>();
        

        public void RunAnimation(SkillCastData data)
        {
            if (data.AtTime(startTime))
            {
                _RunAnimation(data);
            }
        }

        public bool AnimationIsFinished(SkillCastData data)
        {
            return data.PastTime(startTime);
        }

        private void _RunAnimation(SkillCastData data)
        {
            switch (animationType)
            {
                case SkillAnimationType.PlayAnimation:
                    data.caster.PlayAnimation(animationName, animationCrossfade);
                    break;
                case SkillAnimationType.PlaySound:
                    data.caster.PlayAudio(clip);
                    break;
                case SkillAnimationType.ActivateWeapon:
                    data.caster.ActivateWeaponHitbox(effects, data.skill.potency);
                    break;
                case SkillAnimationType.DeactivateWeapon:
                    data.caster.DeactivateWeaponHitbox();
                    break;
            }
        }





        /*
            Called during serialization
             */

        public void UpdateDescription()
        {
            switch (animationType)
            {
                case SkillAnimationType.PlayAnimation:
                    description = $"t={startTime}s - {animationType.ToString()} - {animationName}";
                    break;
                case SkillAnimationType.PlaySound:
                    description = $"t={startTime}s - {animationType.ToString()} - {clip.name}";
                    break;
                case SkillAnimationType.ActivateWeapon:
                    description = $"t={startTime}s - Activate Weapon";
                    break;
                case SkillAnimationType.DeactivateWeapon:
                    description = $"t={startTime}s - Deactivate Weapon";
                    break;
            }
        }

        public void OnBeforeSerialize()
        {
            if (Application.isEditor)
            {
                UpdateDescription();
            }
        }

        public void OnAfterDeserialize()
        {
            
        }
    }
}
