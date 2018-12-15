using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Behaviour
{
    [CreateAssetMenu(menuName = "Conditions/IsDead")]
    public class IsDead : Condition
    {
        public override bool CheckCondition(CharacterManager state)
        {
            return state.isDead;
        }
        
    }
}