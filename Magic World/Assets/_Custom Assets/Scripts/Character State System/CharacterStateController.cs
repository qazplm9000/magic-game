using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharacterStateSystem
{
    public class CharacterStateController : MonoBehaviour
    {

        public CharacterStateTree stateTree;

        public CharacterState currentState
        {
            get { return _currentState; }
            set
            {
                if (_currentState != null)
                {
                    //loop through exit statements of current state
                }

                _currentState = value;

                if (value != null)
                {
                    //loop through enter statements of new state
                    //set currentTransitions to new list
                }
            }
        }

        private CharacterState _currentState;

        private List<StateTransition> currentTransitions = new List<StateTransition>();

        private CharacterManager manager;

        // Use this for initialization
        void Start()
        {
            currentState = stateTree.states[0];
            manager = transform.GetComponent<CharacterManager>();
        }

        // Update is called once per frame
        void Update()
        {
            for (int i = 0; i < currentState.updateEvents.Count; i++) {
                StateEventObject eventObject = currentState.updateEvents[i];

                /*if (eventObject.conditions == null || eventObject.conditions.Execute(manager)) {
                    eventObject.stateEvent.Execute(manager);
                    Debug.Log("Played event");
                }*/
            }
        }
    }
}