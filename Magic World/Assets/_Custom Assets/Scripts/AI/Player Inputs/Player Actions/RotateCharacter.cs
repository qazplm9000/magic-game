using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace InputSystem
{
    [CreateAssetMenu(menuName = "Input/Actions/Rotate Character")]
    public class RotateCharacter : PlayerAction
    {
        public override void Execute(CharacterManager character)
        {
            //Debug.Log("Rotating");
            character.Rotate(character.turnDirection);

        }
    }
}