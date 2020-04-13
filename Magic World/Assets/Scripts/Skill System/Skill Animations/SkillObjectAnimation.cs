using CombatSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SkillSystem
{
    [System.Serializable]
    public class SkillObjectAnimation
    {
        public SkillObjectAnimationType animationType;
        public float delay;
        public bool playEveryFrame;
        
        public float movementSpeed = 5;
        public float rotationSpeed = 100;

        public SkillObjectAnimation(SkillObjectAnimation anim) {
            animationType = anim.animationType;
            delay = anim.delay;
            playEveryFrame = anim.playEveryFrame;
            movementSpeed = anim.movementSpeed;
            rotationSpeed = anim.rotationSpeed;
        }
    }
}