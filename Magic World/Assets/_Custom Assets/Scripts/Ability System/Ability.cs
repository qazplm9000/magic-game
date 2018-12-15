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

    }
}