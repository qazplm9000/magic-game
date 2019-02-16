using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace InputSystem
{
    [CreateAssetMenu(menuName = "Input/Actions/Character Attack")]
    public class CharacterAttack : PlayerAction
    {
        public override void Execute(CharacterManager character)
        {
            character.Attack();
        }
    }
}