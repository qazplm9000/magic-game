using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InputSystem
{
    [CreateAssetMenu(menuName = "Input/Actions/Move Buffer")]
    public class MoveBuffer : PlayerAction
    {
        public override void Execute(CharacterManager character)
        {
            character.currentAction = character.bufferedAction;
            character.bufferedAction = CharacterAction.None;
        }
    }
}