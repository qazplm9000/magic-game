using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace AbilitySystem
{
    //[CreateAssetMenu(menuName = "")]
    public abstract class BattleAnimationEffect : ScriptableObject
    {

        public abstract bool Execute(CharacterManager character, float lastFrame, float currentFrame);

    }
}