using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AbilitySystem
{
    [System.Serializable]
    public class AbilityObject
    {

        public GameObject gameObject;
        public float startTime;
        public float endTime;
        public List<BehaviourData> behaviours;


        public bool Execute(AbilityCaster caster, GameObject go, float previousFrame, float currentFrame) {


            return true;
        }

    }
}