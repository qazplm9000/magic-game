using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace AbilitySystem {
    [CreateAssetMenu(menuName = "Ability/Ability")]
    public class Ability : ScriptableObject {

        public string spellName = "";
        public int manaCost = 1;
        public List<AbilityType> types = new List<AbilityType>();
        public List<BehaviourData> characterBehaviours = new List<BehaviourData>();
        public List<AbilityObject> abilityObjects = new List<AbilityObject>();

        
        /// <summary>
        /// Returns true while ability is running
        /// Returns false when ability is done running
        /// </summary>
        /// <param name="character"></param>
        /// <returns></returns>
        public bool Execute(CharacterManager character) {
            bool result = false;

            character.castPrevious = character.castCurrent;
            character.castCurrent += Time.deltaTime; // later make it possible to multiply by character speed

            //loops through all spell behaviours
            for (int i = 0; i < characterBehaviours.Count; i++) {
                BehaviourData cur = characterBehaviours[i];

                result = result || cur.Execute(character, null, character.castPrevious, character.castCurrent);
            }

            //resets cast times on last frame
            if (!result) {
                character.castPrevious = 0;
                character.castCurrent = 0;
            }

            return result;
        }


    }
}