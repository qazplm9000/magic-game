using CharacterStateSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InputSystem
{
    [CreateAssetMenu(menuName = "Input/State Actions/Go To")]
    public class GoToState : PlayerInputAction
    {
        public enum State {
            Attack,
            Default
        }

        public State state;

        public override void Execute(CharacterManager character)
        {
            CharacterState newState = state == State.Attack ? character.attackState : character.defaultState;
            character.currentState.ExitState(character, newState);
        }
    }
}