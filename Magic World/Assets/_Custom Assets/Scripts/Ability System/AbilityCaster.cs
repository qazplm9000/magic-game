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
        private List<GameObject> castObjects = new List<GameObject>();
        

        // Use this for initialization
        void Start()
        {
            manager = transform.GetComponent<CharacterManager>();

            //Make castObjects a list of 10 null objects
            for (int i = 0; i < 10; i++) {
                castObjects.Add(null);
            }
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

            //animate player
            for (int i = 0; i < currentAbility.characterBehaviours.Count; i++) {
                /*currentAbility.characterBehaviours[i].Execute(  manager.caster, 
                                                                manager.transform.gameObject, 
                                                                previousFrame, currentFrame);*/
            }

            //animate all other objects
            for (int i = 0; i < currentAbility.abilityObjects.Count; i++) {
                AbilityObject currentObject = currentAbility.abilityObjects[i];
                //Insert the initial transform for the object
                if (previousFrame < currentObject.startTime && currentFrame > currentObject.startTime) {
                    castObjects[i] = ObjectPool.pool.PullObject(currentObject.gameObject);
                }

                //checks if the animation has ended or not
                if (castObjects[i] != null)
                {
                    if (previousFrame > currentObject.endTime)
                    {
                        ObjectPool.pool.RemoveObject(castObjects[i]);
                        castObjects[i] = null;
                        continue;
                    }
                }
                else {

                }

                //loop through all behaviours
                for (int j = 0; j < currentObject.behaviours.Count; j++) {
                    BehaviourData behaviour = currentObject.behaviours[j];

                    if (!behaviour.HasExecuted(previousFrame, currentFrame))
                    {
                        //behaviour.Execute(manager.caster, castObjects[i], previousFrame, currentFrame);
                    }
                }
            }

            return result;
        }

        private void InitCast() {

        }


        private void Reset() {
            previousFrame = 0;
            currentFrame = 0;
            currentAbility = null;
            casting = false;
        }

        
    }
}