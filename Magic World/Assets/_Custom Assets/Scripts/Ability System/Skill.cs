using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace AbilitySystem
{
    [System.Serializable]
    public class Skill
    {
        public CharacterManager caster;
        public List<CharacterManager> targets;
        public Ability ability;

        public float previousFrame = 0;
        public float currentFrame = 0;




    }
}