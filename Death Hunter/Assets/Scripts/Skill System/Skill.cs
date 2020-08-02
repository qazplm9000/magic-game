using EffectSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SkillSystem
{
    //[CreateAssetMenu(menuName = "Skill/Skill", fileName = "Skill")]
    public abstract class Skill : ScriptableObject
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


        public void RunSkill(SkillCastData data)
        {
            data.Tick();
        }
        
        
    }


    public enum SkillAnimationType
    {
        PlayAnimation,
        PlaySound,
        CreateObject
    }

    [System.Serializable]
    public class SkillAnimation
    {
        public SkillAnimationType animationType;
        public float startTime;

        //Used for Play Animation
        [Tooltip("Name of animation to play")]
        public string animationName;

        //Used for Play Sound
        [Tooltip("Sound to play")]
        public AudioClip clip;

        //Used for Create Object
        [Tooltip("Index of object to instantiate")]
        public float lifetime;
        public int objIndex;
        public SkillObjectLocation location;
        public SkillObjectParent parent;
        public Vector3 positionOffset;
        public Vector3 rotationOffset;
        public bool moveForwards = true;
        public float movementSpeed = 5;
        public bool rotateTowardsTarget = true;
        public float rotationSpeed = 100;
        public float scale = 1;
        public bool destroyOnCollision = false;
        public List<int> effectIds;

        public void RunAnimation(SkillCastData data)
        {
            if(data.previousFrame <= startTime && data.currentFrame > startTime)
            {
                _RunAnimation(data);
            }
        }

        public bool AnimationIsFinished(SkillCastData data)
        {
            return data.currentFrame > startTime;
        }

        private void _RunAnimation(SkillCastData data)
        {
            switch (animationType)
            {
                case SkillAnimationType.PlayAnimation:
                    data.caster.PlayAnimation(animationName);
                    break;
                case SkillAnimationType.PlaySound:
                    data.caster.PlayAudio(clip);
                    break;
                case SkillAnimationType.CreateObject:

                    break;
            }
        }
    }
}