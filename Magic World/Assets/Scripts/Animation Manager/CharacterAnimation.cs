using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace AnimationSystem
{
    [System.Serializable]
    public class CharacterAnimation
    {
        [Tooltip("Name to be called in the animation manager")]
        public string characterAnimName;
        [Tooltip("Name of the animation in the Animator component")]
        public string animationName;
    }
}