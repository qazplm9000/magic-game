using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharacterStateSystem
{
    [CreateAssetMenu(menuName = "State/State Tree")]
    public class CharacterStateTree : ScriptableObject
    {
        public List<CharacterState> states;


        public List<StateTransition> transitions;

        private Dictionary<CharacterState, List<StateTransition>> _transitions;

        public CharacterState defaultState;


        //initializes the dictionary for the list of states
        public void Init()
        {
            InitDict();   
            //Debug.Log("There are " + _transitions.Count + " states in the dictionary");
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
                    character.currentState = states[0];
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
        /*public bool ChangeState(CharacterManager character, string stateName = "idle") {
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
        }*/


        public CharacterState GetDefaultState() {
            return defaultState;
        }

        public List<StateTransition> GetTransitions(CharacterState state) {
            List<StateTransition> result = new List<StateTransition>();

            if (_transitions.ContainsKey(state)) {
                result = _transitions[state];
            }

            return result;
        }


        private void InitDict() {
            _transitions = new Dictionary<CharacterState, List<StateTransition>>();

            //add keys in
            for (int i = 0; i < states.Count; i++) {
                _transitions[states[i]] = new List<StateTransition>();
            }


            //adds transitions into keys
            for (int i = 0; i < transitions.Count; i++) {
                StateTransition transition = transitions[i];
                //Debug.Log("Added transition");
                _transitions[transition.startingState].Add(transition);
            }


        }


    }
}