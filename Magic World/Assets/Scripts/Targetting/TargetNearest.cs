using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TargettingSystem
{
    [CreateAssetMenu(menuName = "Modules/Targetting/Target Nearest")]
    public class TargetNearest : Targetter
    {
        public override float GetTargetCriteria(CharacterManager character, CharacterManager target)
        {
            return (character.transform.position - target.transform.position).magnitude;
        }
    }
}