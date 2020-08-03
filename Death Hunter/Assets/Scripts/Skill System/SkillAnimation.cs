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
                    SkillObject createdObj = CreateSkillObject(objIndex);
                    Transform locationTransform = GetLocationTransform(data, location);
                    SetObjectTransform(locationTransform, createdObj);
                    SetObjectRotation(locationTransform, createdObj);
                    SetObjectScale(locationTransform, createdObj);

                    SkillObjectData soData = new SkillObjectData(data, objEffects, lifetime);
                    createdObj.StartSkill(soData);
                    break;
            }
        }

        private void SetObjectTransform(Transform location, SkillObject createdObj){
            createdObj.transform.position = location.position +
                                            location.forward * positionOffset.z +
                                            location.right * positionOffset.x +
                                            location.up * positionOffset.y;
        }

        private void SetObjectRotation(Transform location, SkillObject createdObj)
        {
            createdObj.transform.rotation = location.transform.rotation *
                                            Quaternion.Euler(rotationOffset.x, 0, 0) *
                                            Quaternion.Euler(0, rotationOffset.y, 0) *
                                            Quaternion.Euler(0, 0, rotationOffset.z);
        }

        private void SetObjectScale(Transform location, SkillObject createdObj)
        {
            createdObj.transform.localScale = new Vector3(scale, scale, scale);
        }

        private Transform GetLocationTransform(SkillCastData data, SkillObjectLocation location)
        {
            Transform result = null;

            switch (location)
            {
                case SkillObjectLocation.Creator:
                    result = data.caster.transform;
                    break;
                case SkillObjectLocation.Caster:
                    result = data.caster.transform;
                    break;
                case SkillObjectLocation.Target:
                    result = data.target.transform;
                    break;
            }

            return result;
        }

        private SkillObject CreateSkillObject(int searchedIndex)
        {
            SkillObject searchedObj = WorldManager.GetSkillObjectByID(objIndex);
            SkillObject result = GameObject.Instantiate<SkillObject>(searchedObj);

            return result;
        }




        /*
            Called during serialization
             */

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
