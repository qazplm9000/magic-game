using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InputSystem
{
    [CreateAssetMenu(menuName = "Input/Actions/Target Nearest")]
    public class TargetNearest : PlayerAction
    {
        public override void Execute(CharacterManager character)
        {
            //character.targetter.target = character.targetter.GetNearestTarget();
            character.target = character.targetter.GetTarget(character);
        }
    }
}