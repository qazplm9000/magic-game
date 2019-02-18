using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TargettingSystem
{
    [CreateAssetMenu(menuName = "Modules/Targetting/Target Nearest")]
    public class TargetNearest : TargetCriteria
    {
        public override float GetCriteria(CharacterManager character, CharacterManager target)
        {
            return (character.transform.position - target.transform.position).magnitude;
        }
    }
}