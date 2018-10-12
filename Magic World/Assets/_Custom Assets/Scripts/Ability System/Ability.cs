using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace AbilitySystem {
    [CreateAssetMenu(menuName = "Ability/Ability")]
    public class Ability : ScriptableObject {

        public string spellName = "";
        public List<BehaviourData> characterBehaviours = new List<BehaviourData>();
        public List<AbilityObject> abilityObjects = new List<AbilityObject>();

        /// <summary>
        /// Returns true when running
        /// Returns false when done running
        /// </summary>
        /// <returns></returns>
        public bool Execute(AbilityCaster caster, float previousFrame, float currentFrame) {
            bool result = false;



            /*for (int i = 0; i < characterBehaviours.Count; i++) {
                //calls if behaviour has not executed yet
                if (!characterBehaviours[i].HasExecuted(previousFrame, currentFrame)) {
                    result = true;

                    //Executes behaviour if is currently running
                    if (characterBehaviours[i].IsRunning(previousFrame, currentFrame)) {
                        characterBehaviours[i].Execute(caster, previousFrame, currentFrame);
                    }
                }
            }*/
            
            return result;
        }

    }
}