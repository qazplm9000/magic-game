using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace InputSystem
{

    [CreateAssetMenu(menuName = "Input/Actions/Set Action")]
    public class SetAction : PlayerAction
    {

        public CharacterAction action;

        public override void Execute(CharacterManager character)
        {
            if (character.currentAction == CharacterAction.None)
            {
                character.currentAction = action;
            }
            else {
                character.bufferedAction = action;
            }
        }
    }
}