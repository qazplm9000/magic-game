using CombatSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace NewSkillSystem
{
    [System.Serializable]
    public class SkillObject
    {
        public GameObject skillObject;
        public SkillLocation location;
        [Tooltip("X is horizontal, Z is forwards")]
        public Vector3 offset;
        public bool parent;
        public bool applyToCaster;

        [Range(0, 5)]
        public float startTime;
        [Range(0,5)]
        public float maxDuration;


        public GameObject CreateSkillObject(CastManager caster, CastManager target) {
            GameObject result = null;

            if (skillObject != null)
            {
                result = GameObject.Instantiate(skillObject);
                GameObject castLocation = GetLocationObject(caster, target);
                result.transform.rotation = castLocation.transform.rotation;
                result.transform.position = castLocation.transform.position;

                result.transform.position += castLocation.transform.forward * offset.z
                                            + castLocation.transform.right * offset.x
                                            + castLocation.transform.up * offset.y;

                if (parent)
                {
                    result.transform.parent = castLocation.transform;
                }
            }

            return result;
        }


        private GameObject GetLocationObject(Combatant caster, Combatant target) {
            GameObject result = null;

            if (applyToCaster)
            {
                result = caster.gameObject;
            }
            else {
                result = target.gameObject;
            }

            return result;
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

        public bool IsRunning(float previousFrame, float currentFrame) {
            bool result = false;

            return result;
        }

        public bool IsDone(float previousFrame, float currentFrame) {
            bool result = true;

            return result;
        }

    }

}