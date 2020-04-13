using CombatSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SkillSystem
{
    [System.Serializable]
    public class SkillObjectData
    {
        public SkillObject skillObject;
        [Tooltip("X is horizontal, Z is forwards")]
        public Vector3 positionOffset;
        public Vector3 rotationOffset;
        public float scale = 1;
        public SkillObjectLocation location;
        public SkillObjectParent parent;
        public bool destroyOnCollision = false;

        public List<SkillObjectAnimation> animations;
        public List<SkillEffect> effects;



        [Range(0, 5)]
        public float startTime;
        [Range(0,5)]
        public float lifetime;

        public float speed = 0;

        /// <summary>
        /// Creates a skill object by the Combatant caster
        /// </summary>
        /// <param name="caster"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public SkillObject CreateSkillObject(Combatant caster, Combatant target, SkillObject obj = null) {
            SkillObject result = null;

            if (skillObject == null) {
                Debug.LogError("No object attached to skill object!");
                return null;
            }

            result = GameObject.Instantiate(skillObject);
            Debug.Log("Remember to use a pool system here");
            GameObject castLocation = GetCastLocation(caster, target, obj);

            SetObjectPosition(result, castLocation);
            SetObjectRotation(result, castLocation);
            SetObjectScale(result);
            
            result.StartSkill(caster, target, this);

            result.transform.parent = GetParentTransform(caster, target, obj);

            return result;
        }



        private GameObject GetCastLocation(Combatant caster, Combatant target, SkillObject obj)
        {
            GameObject result = null;

            switch (location)
            {
                case SkillObjectLocation.Creator:
                    if (obj != null)
                    {
                        result = obj.gameObject;
                    }
                    else {
                        result = caster.gameObject;
                    }
                    break;
                case SkillObjectLocation.Caster:
                    result = caster.gameObject;
                    break;
                case SkillObjectLocation.Target:
                    result = target.gameObject;
                    break;
            }

            return result;
        }

        private Transform GetParentTransform(Combatant caster, Combatant target, SkillObject obj) {
            Transform result = null;

            switch (parent)
            {
                case SkillObjectParent.NoParent:
                    break;
                case SkillObjectParent.ParentToCaster:
                    result = caster.transform;
                    break;
                case SkillObjectParent.ParentToTarget:
                    result = target.transform;
                    break;
                case SkillObjectParent.ParentToCreator:
                    result = obj == null ? caster.transform : obj.transform;
                    break;
            }

            return result;
        }

        private void SetObjectPosition(SkillObject obj, GameObject castLocation) {
            obj.transform.position = castLocation.transform.position +
                                     castLocation.transform.forward * positionOffset.z +
                                     castLocation.transform.right * positionOffset.x +
                                     castLocation.transform.up * positionOffset.y;
        }

        private void SetObjectRotation(SkillObject obj, GameObject castLocation) {
            obj.transform.rotation = castLocation.transform.rotation * Quaternion.Euler(rotationOffset.x, rotationOffset.y, rotationOffset.z);
        }

        private void SetObjectScale(SkillObject obj) {
            obj.transform.localScale = new Vector3(scale, scale, scale);
        }


        public bool IsStarting(float previousFrame, float currentFrame) {
            bool result = false;

            if (startTime == 0)
            {
                if (currentFrame == 0)
                {
                    result = true;
                }
            }
            else if (previousFrame <= startTime && currentFrame > startTime) {
                result = true;
            }

            return result;
        }

        public bool HasStarted(float previousFrame, float currentFrame) {
            return previousFrame > startTime;
        }

        public bool IsRunning(float previousFrame, float currentFrame) {
            return currentFrame < lifetime;
        }

        public bool IsDone(float previousFrame, float currentFrame) {
            return currentFrame > lifetime;
        }


        public List<SkillEffect> CopyEffects() {
            List<SkillEffect> result = new List<SkillEffect>(effects.Count);

            for (int i = 0; i < effects.Count; i++)
            {
                result.Add(new SkillEffect(effects[i]));
            }

            return result;
        }

        public List<SkillObjectAnimation> CopyAnimations() {
            List<SkillObjectAnimation> result = new List<SkillObjectAnimation>(animations.Count);

            for (int i = 0; i < animations.Count; i++) {
                result.Add(new SkillObjectAnimation(animations[i]));
            }

            return result;
        }


    }
}