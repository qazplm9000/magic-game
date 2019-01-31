using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharacterStateSystem
{
    [CreateAssetMenu(menuName = "State/State Tree")]
    public class CharacterStateTree : ScriptableObject
    {
        [System.Serializable]
        public class StateNames {
            public string stateName;
            public CharacterState state;
        }

        public List<StateNames> states;
        private Dictionary<string, CharacterState> _states;

        //initializes the dictionary for the list of states
        public void OnEnable()
        {
            if (states == null) {
                return;
            }

            _states = new Dictionary<string, CharacterState>();

            for (int i = 0; i < states.Count; i++) {
                StateNames thisState = states[i];
                _states[thisState.stateName] = thisState.state;
            }
        }

        public void Execute(CharacterManager character) {
            //run the character's current state
            if (character.currentState != null)
            {
                character.currentState.Execute(character);
            }
            else {
                //If null, set to first state
                if(states.Count > 0){
                    character.currentState = states[0].state;
                }
            }
        }

        /// <summary>
        /// Changes the character's current state into the state of stateName
        /// Returns true if the state exists, false otherwise
        /// </summary>
        /// <param name="character"></param>
        /// <param name="stateName"></param>
        /// <returns></returns>
        public bool ChangeState(CharacterManager character, string stateName = "default") {
            bool result = false;

            if (_states.ContainsKey(stateName)) {
                if(character.currentState != null)
                {
                    character.currentState.ExitState(character);
                }

                character.currentState = _states[stateName];
                character.currentState.EnterState(character);

                result = true;
            }

            return result;
        }


        public CharacterState GetDefaultState() {
            CharacterState result = null;

            if (_states.ContainsKey("default"))
            {
                result = _states["default"];
            }
            else if (states.Count > 0)
            {
                result = states[0].state;
            }

            return result;
        }

    }
}