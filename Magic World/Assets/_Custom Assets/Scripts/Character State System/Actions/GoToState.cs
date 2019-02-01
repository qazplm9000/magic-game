using CharacterStateSystem;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

namespace InputSystem
{
    [CreateAssetMenu(menuName = "Input/State Actions/Go To")]
    public class GoToState : PlayerAction
    {
        public string stateName;

        public override void Execute(CharacterManager character)
        {
            bool result = character.stateTree.ChangeState(character, stateName);

            if (!result) {
                throw new MissingStateException();
            }
        }


        public class MissingStateException : Exception {
            public MissingStateException() {
            }
        }
    }
}