using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InputSystem;

namespace CharacterStateSystem
{
    [System.Serializable]
    public class StateEventObject
    {
        public ConditionList conditions;
        public PlayerAction stateEvent;

        public void Execute(CharacterManager character) {
            if (conditions.ConditionsPass(character)) {
                stateEvent.Execute(character);
            }
        }
    }
}