using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AbilitySystem
{
    [System.Serializable]
    public class BehaviourData
    {

        public float startTime;
        public float runTime;

        public AbilityBehaviour behaviour;
        public string animationName;
        public GameObject behaviourObject;
        public Vector3 objectPosition;
        public Vector3 objectRotation;

        
        public bool Execute(AbilityCaster caster, GameObject go, float previousFrame, float currentFrame) {
            float adjustedPrevious = previousFrame - startTime;
            float adjustedCurrent = currentFrame - startTime;

            //runs on the first frame
            if (IsStarting(previousFrame, currentFrame)) {
                behaviour.Init(caster, go, this);
                behaviour.Execute(caster, go, this, 0, adjustedCurrent);
            }
            //runs in the middle frames
            else if (IsRunning(previousFrame, currentFrame)) {
                behaviour.Execute(caster, go, this, adjustedPrevious, adjustedCurrent);
            }
            //runs on the last frame
            else if (IsEnding(previousFrame, currentFrame)) {
                behaviour.Execute(caster, go, this, adjustedPrevious, runTime);
                behaviour.End(caster, go, this);
            }

            return !HasExecuted(previousFrame, currentFrame);
        }

        //Returns true once the current frame is outside the effect duration
        public bool HasExecuted(float previousFrame, float currentFrame) {
            return previousFrame > (startTime + runTime);
        }

        //Returns true while current frame and previous frame are inside the effect duration
        public bool IsRunning(float previousFrame, float currentFrame) {
            return (currentFrame > startTime && currentFrame < startTime + runTime) && (previousFrame > startTime && previousFrame < startTime + runTime);
        }

        //Returns true as soon as the behaviour starts
        public bool IsStarting(float previousFrame, float currentFrame) {
            return previousFrame <= startTime && currentFrame >= startTime;
        }

        //Returns true as soon as behaviour ends
        public bool IsEnding(float previousFrame, float currentFrame) {
            return previousFrame <= (startTime + runTime) && currentFrame >= (startTime + runTime);
        }
    }
}