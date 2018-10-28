using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AbilitySystem
{
    public class AbilityCaster : MonoBehaviour
    {

        public CharacterManager manager;
        public List<Ability> abilityList = new List<Ability>();
        public int abilityIndex = 0;
        public bool casting = false;
        public float previousFrame = 0;
        public float currentFrame = 0;

        public Ability currentAbility;
        public Dictionary<int, GameObject> _instantiatedObjects = new Dictionary<int, GameObject>();
        

        // Use this for initialization
        void Start()
        {
            manager = transform.GetComponent<CharacterManager>();
        }

        // Update is called once per frame
        void Update()
        {
            if (casting)
            {
                Execute();
            }
        }

        /// <summary>
        /// Changes the current spell index
        /// </summary>
        /// <param name="amount"></param>
        /// <param name="increment"></param>
        public void ChangeIndex(int amount = 1, bool increment = true)
        {
            if (increment)
            {
                abilityIndex += amount;
            }
            else
            {
                abilityIndex -= amount;
            }

            //add modulo and way to check if 0
        }

        /// <summary>
        /// Casts the currently selected spell
        /// Returns false if the character is unable to cast
        /// </summary>
        /// <returns></returns>
        public bool Cast() {
            bool result = false;

            if (!casting)
            {
                currentAbility = abilityList[abilityIndex];
                //add in way to check if spell exists
                casting = true;
                result = true;
            }

            return result;
        }


        private bool Execute()
        {
            bool result = true;
            previousFrame = currentFrame;
            currentFrame += Time.deltaTime;

            /*result = currentAbility.Execute(this, previousFrame, currentFrame);

            if (!result) {
                Reset();
            }
            */
            for (int i = 0; i < currentAbility.characterBehaviours.Count; i++) {
                currentAbility.characterBehaviours[i].Execute(  manager.caster, 
                                                                manager.transform.gameObject, 
                                                                previousFrame, currentFrame);
            }

            for (int i = 0; i < currentAbility.abilityObjects.Count; i++) {
                AbilityObject currentObject = currentAbility.abilityObjects[i];
                //Insert the initial transform for the object
                GameObject go = ObjectPool.pool.PullObject(currentObject.go);

                for (int j = 0; j < currentObject.behaviours.Count; j++) {
                    BehaviourData behaviour = currentObject.behaviours[j];

                    if (!behaviour.HasExecuted(previousFrame, currentFrame))
                    {
                        behaviour.Execute(manager.caster, go, previousFrame, currentFrame);
                    }
                }
            }

            return result;
        }


        private void Reset() {
            previousFrame = 0;
            currentFrame = 0;
            currentAbility = null;
            casting = false;
        }

        /*
        //readjust these functions so that BehaviourData is only for data
        public bool Execute(AbilityCaster caster, GameObject go, float previousFrame, float currentFrame)
        {
            bool result = true;
            float adjustedPrevious = previousFrame - startTime;
            float adjustedCurrent = currentFrame - startTime;

            //runs on the first frame
            if (IsStarting(previousFrame, currentFrame))
            {
                behaviour.Init(caster, go, this);
                behaviour.Execute(caster, go, this, 0, adjustedCurrent);
            }
            //runs in the middle frames
            else if (IsRunning(previousFrame, currentFrame))
            {
                behaviour.Execute(caster, go, this, adjustedPrevious, adjustedCurrent);
            }
            //runs on the last frame
            else if (IsEnding(previousFrame, currentFrame))
            {
                behaviour.Execute(caster, go, this, adjustedPrevious, runTime);
                behaviour.End(caster, go, this);
            }

            return result;
        }

        //Returns true once the previous frame is outside the effect duration
        public bool HasExecuted(float previousFrame, float currentFrame)
        {
            return previousFrame > (startTime + runTime);
        }

        //Returns true while current frame and previous frame are inside the effect duration
        public bool IsRunning(float previousFrame, float currentFrame)
        {
            return (currentFrame > startTime && currentFrame < startTime + runTime) && (previousFrame > startTime && previousFrame < startTime + runTime);
        }

        //Returns true as soon as the behaviour starts
        public bool IsStarting(float previousFrame, float currentFrame)
        {
            return previousFrame <= startTime && currentFrame >= startTime;
        }

        //Returns true as soon as behaviour ends
        public bool IsEnding(float previousFrame, float currentFrame)
        {
            return previousFrame <= (startTime + runTime) && currentFrame >= (startTime + runTime);
        }

    */

    }
}