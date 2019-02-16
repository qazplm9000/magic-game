using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharacterStateSystem
{
    [System.Serializable]
    public class StateTransition
    {

        public ConditionList conditions;
        public CharacterState startingState;
        public CharacterState endingState;

        public bool Transition(CharacterStateManager manager) {
            bool result = conditions.ConditionsPass(manager.character);

            if (result) {
                manager.ChangeState(endingState);
            }

            return result;
        }

    }
}