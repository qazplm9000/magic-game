using CombatSystem.CastLocationSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace CombatSystem.SkillSystem
{
    [System.Serializable]
    public class SkillObject
    {
        [Tooltip("Gameobject to instantiate")]
        public GameObject go;
        [Tooltip("Instantiates relative to the target instead of the caster")]
        public bool relativeToTarget = false;
        [Tooltip("Location where the object will be instantiated")]
        public CastLocation castLocation;
        [Tooltip("Offset relative to cast location")]
        public Vector3 offset;
        [Tooltip("Toggle to parent object to the target")]
        public bool parentToTarget = false;

        [Space(10)]

        [Tooltip("Time the object gets instantiated")]
        public float startTime;
        [Tooltip("How long the object lasts for")]
        public float lifetime;
        [Tooltip("Toggle to make the object disappear on collision")]
        public bool killOnCollision = false;
        [Tooltip("Controller for the object")]
        public SkillObjectController controller;
        
    }
}