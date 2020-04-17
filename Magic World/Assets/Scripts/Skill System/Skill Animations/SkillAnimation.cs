using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SkillSystem
{
    [System.Serializable]
    public class SkillAnimation : ISerializationCallbackReceiver
    {
        public SkillAnimationType animationType;
        public float startTime;

        //Used for Play Animation
        [Tooltip("Name of animation to play")]
        public string animationName;

        //Used for Play Sound
        [Tooltip("Sound to play")]
        public AudioClip sound;

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

        public void OnAfterDeserialize()
        {
            
        }

        public void OnBeforeSerialize()
        {
            //throw new System.NotImplementedException();
        }
    }
}