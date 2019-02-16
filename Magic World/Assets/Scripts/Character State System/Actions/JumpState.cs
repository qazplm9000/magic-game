using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CharacterStateSystem;

namespace InputSystem
{
    [CreateAssetMenu(menuName = "Input/Actions/Jump To State")]
    public class JumpState : PlayerAction
    {
        public CharacterState jumpState;

        public override void Execute(CharacterManager character)
        {
            character.stateManager.ChangeState(jumpState);
        }
    }
}