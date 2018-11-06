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
        private List<List<BehaviourData>> currentBehaviours = new List<List<BehaviourData>>();
        private List<List<BehaviourData>> pendingBehaviours = new List<List<BehaviourData>>();

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
        public bool Cast(Ability newAbility) {
            bool result = false;

            if (!casting)
            {
                currentAbility = newAbility;
                InitLists(currentAbility);
                casting = true;
                result = true;
            }

            return result;
        }


        private bool Execute()
        {
            bool running = false;
            previousFrame = currentFrame;
            currentFrame += Time.deltaTime;

            
            //animate player
            for (int i = 0; i < currentAbility.characterBehaviours.Count; i++) {
                BehaviourData currentBehaviour = currentAbility.characterBehaviours[i];

                running = running || currentBehaviour.Execute(  manager.caster2, 
                                                                manager.transform.gameObject, 
                                                                previousFrame, currentFrame);
            }


            //check all pending behaviours
            for (int i = 0; i < currentAbility.abilityObjects.Count; i++) {
                running = running || StartBehaviours(i, previousFrame, currentFrame);
            }

            //execute all current behaviours
            for (int i = 0; i < currentAbility.abilityObjects.Count; i++) {
                running = running || RunBehaviours(i, previousFrame, currentFrame);
            }

            return running;
        }

        

        /// <summary>
        /// Loops through to make sure all behaviours and puts them in the current list
        /// Returns true if still running
        /// </summary>
        /// <param name="index"></param>
        /// <param name="previousFrame"></param>
        /// <param name="currentFrame"></param>
        /// <returns></returns>
        private bool StartBehaviours(int index, float previousFrame, float currentFrame) {
            bool result = false;
            
            while (pendingBehaviours[index].Count > 0) {
                BehaviourData currentBehaviour = pendingBehaviours[index][0];

                if (currentBehaviour.startTime < currentFrame)
                {
                    currentBehaviours[index].Add(currentBehaviour);
                    pendingBehaviours[index].RemoveAt(0);
                }
                else {
                    result = true;
                    break;
                }
            }

            return result;
        }

        /// <summary>
        /// Runs all current behaviours, removing any that have finished
        /// Returns true if still running
        /// </summary>
        /// <param name="index"></param>
        /// <param name="previousFrame"></param>
        /// <param name="currentFrame"></param>
        /// <returns></returns>
        private bool RunBehaviours(int index, float previousFrame, float currentFrame) {
            List<BehaviourData> behaviours = currentBehaviours[index];

            int i = 0;
            while (i < behaviours.Count) {
                behaviours[i].Execute(manager.caster2, castObjects[index], previousFrame, currentFrame);

                if (!behaviours[i].HasExecuted(previousFrame, currentFrame)) {
                    behaviours.RemoveAt(i);
                    continue;
                }

                i++;
            }

            return behaviours.Count != 0;
        }



        /// <summary>
        /// Initializes the lists for the ability to handle
        /// </summary>
        /// <param name="ability"></param>
        private void InitLists(Ability ability) {
            castObjects = new List<GameObject>();
            currentBehaviours = new List<List<BehaviourData>>();
            pendingBehaviours = new List<List<BehaviourData>>();

            List<AbilityObject> ao = ability.abilityObjects;
            for (int i = 0; i < ao.Count; i++) {
                castObjects.Add(null);
                currentBehaviours.Add(new List<BehaviourData>());
                pendingBehaviours.Add(new List<BehaviourData>());

                for (int j = 0; j < ao[i].behaviours.Count; j++) {
                    pendingBehaviours[i].Add(ao[i].behaviours[j]);
                }
            }
        }


        private void InitObject(GameObject go, int index) {
            GameObject newObject = ObjectPool.pool.PullObject(go);
            castObjects[index] = go;
        }
        


        private void Reset() {
            previousFrame = 0;
            currentFrame = 0;
            currentAbility = null;
            casting = false;
        }

        
    }
}