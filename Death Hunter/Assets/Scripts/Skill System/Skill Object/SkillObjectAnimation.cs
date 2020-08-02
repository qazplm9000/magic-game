using CombatSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SkillSystem
{
    public enum SkillObjectAnimationType
    {
        MoveForwards,
        TurnTowardsTarget,
        ChangeMovementSpeed,
        ChangeRotationSpeed,
        ChangeMovementAcceleration
    }


    [System.Serializable]
    public class SkillObjectAnimation
    {
        public SkillObjectAnimationType animationType;
        public float startTime;
        public float duration;

        public SkillObjectAnimation(SkillObjectAnimation anim) {
            animationType = anim.animationType;
            startTime = anim.startTime;
            duration = anim.duration;
        }
    }
}