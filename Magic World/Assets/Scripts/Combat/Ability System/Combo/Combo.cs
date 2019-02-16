using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AbilitySystem
{
    
    public abstract class Combo : Ability
    {
        [Tooltip("Animation used for casting")]
        public string animationName = "";
        [Range(0, 1)]
        [Tooltip("How long the hit lasts")]
        public float totalTime;


        //public abstract bool Execute(CharacterManager character, float previousFrame, float currentFrame);
    }
}