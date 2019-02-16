using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace InputSystem
{
    [CreateAssetMenu(menuName = "Input/Actions/Look At Target")]
    public class LookAtTarget : PlayerAction
    {
        public override void Execute(CharacterManager character)
        {
            character.turnDirection = character.target.transform.position - character.transform.position;
        }
    }
}