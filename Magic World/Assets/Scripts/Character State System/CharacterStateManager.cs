using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharacterStateSystem
{
    public class CharacterStateManager : MonoBehaviour
    {
        public CharacterManager character;

        public CharacterStateTree stateTree;
        [Tooltip("Total time spent in the current state")]
        public float stateTime = 0f;


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

        public void Start()
        {
            World.eventManager.SubscribeEvent("OnAttackButton", CheckTransitions);
            World.eventManager.SubscribeEvent("OnCastButton", CheckTransitions);
            World.eventManager.SubscribeEvent("OnFinishCast", CheckTransitions);
            World.eventManager.SubscribeEvent("OnFinishAttack", CheckTransitions);
        }



        public void Update(){
            currentState.Execute(character);
            stateTime += Time.deltaTime;

            
        }



        /// <summary>
        /// Loops through all transitions and transitions if its conditions hold true
        /// </summary>
        public void CheckTransitions() {
            Debug.Log("Checking Transitions");
            for (int i = 0; i < currentTransitions.Count; i++)
            {
                currentTransitions[i].Transition(this);
            }
        }


        public void ChangeState(CharacterState state) {
            if (state != null)
            {
                if (currentState != null)
                {
                    currentState.ExitState(character);
                }

                state.EnterState(character);
                currentState = state;
                currentTransitions = stateTree.GetTransitions(state);

                stateTime = 0;

                character.RaiseEvent("OnStateChanged");
            }

        }


    }
}