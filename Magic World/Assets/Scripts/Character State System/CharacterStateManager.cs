using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharacterStateSystem
{
    public class CharacterStateManager : MonoBehaviour
    {
        public CharacterManager character;

        public CharacterStateTree stateTree;


        public CharacterState currentState;
        public List<StateTransition> currentTransitions;


        public void Awake()
        {
            character = transform.GetComponent<CharacterManager>();

            if (stateTree != null) {
                stateTree.Init();
            }

            ChangeState(stateTree.GetDefaultState());
        }



        public void Update()
        {
            currentState.Execute(character);

            //Loops through all transitions and transitions if its conditions hold true
            for (int i = 0; i < currentTransitions.Count; i++) {
                currentTransitions[i].Transition(this);
            }
        }



        public void ChangeState(CharacterState state) {
            if (currentState != null) {
                currentState.ExitState(character);
            }

            if (state != null)
            {
                state.EnterState(character);
                currentState = state;
                currentTransitions = stateTree.GetTransitions(state);
            }

        }


    }
}