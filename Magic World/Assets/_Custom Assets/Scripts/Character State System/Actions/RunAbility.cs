using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InputSystem
{
    [CreateAssetMenu(menuName = "Input/Character State/Run Ability")]
    public class RunAbility : PlayerInputAction
    {
        public override void Execute(CharacterManager character)
        {
            bool result = character.RunAbility();
            
        }
    }
}