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
        CreateObject
    }

    [System.Serializable]
    public class SkillAnimation
    {
        public string description = "";
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
        public List<SkillEffect> objEffects = new List<SkillEffect>();

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
                    data.caster.PlayAnimation(animationName);
                    break;
                case SkillAnimationType.PlaySound:
                    data.caster.PlayAudio(clip);
                    break;
                case SkillAnimationType.CreateObject:
                    SkillObject searchedObj = WorldManager.GetSkillObjectByID(objIndex);
                    if (searchedObj != null)
                    {
                        SkillObject temp = GameObject.Instantiate<SkillObject>(searchedObj);
                        temp.transform.position = data.caster.transform.position + positionOffset;
                        temp.transform.rotation = data.caster.transform.rotation;
                        SkillObjectData soData = new SkillObjectData(data, objEffects, lifetime);
                        temp.StartSkill(soData);
                    }
                    break;
            }
        }


        public void UpdateDescription()
        {
            switch (animationType)
            {
                case SkillAnimationType.PlayAnimation:
                    description = $"{startTime} - {animationType.ToString()} - {animationName}";
                    break;
                case SkillAnimationType.PlaySound:
                    description = $"{startTime} - {animationType.ToString()} - {clip.name}";
                    break;
                case SkillAnimationType.CreateObject:
                    description = $"{startTime} - {animationType.ToString()} - {objIndex}";
                    break;
            }
        }
    }
}