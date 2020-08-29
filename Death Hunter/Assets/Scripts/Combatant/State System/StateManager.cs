using CombatSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace StateSystem
{
    

    public class StateManager : MonoBehaviour
    {
        public StateData stateData;
        public CharacterStateSO states;
        public Combatant character;

        private void Awake()
        {
            character = transform.GetComponent<Combatant>();
        }

        // Start is called before the first frame update
        void Start()
        {
            stateData = new StateData();
            stateData.characterFlags = transform.GetComponent<FlagManager>();
            SetupStartState();
        }

        // Update is called once per frame
        void Update()
        {
            stateData.currentDuration += Time.deltaTime;
            stateData.currentState.RunState(character);
        }

        private void LateUpdate()
        {
            CheckIfStateChanged();
        }

        public void ChangeFlag(Flag flag, bool value)
        {
            stateData.characterFlags.SetFlag(flag, value);
        }

        public bool GetFlag(Flag flag)
        {
            return stateData.characterFlags.GetFlag(flag);
        }

        public void ResetState()
        {
            ChangeState(states.defaultState);
        }


        /*
            Private functions
             */

        private void SetupStartState()
        {
            ChangeState(states.defaultState);
        }

        private void CheckIfStateChanged()
        {
            State newState = states.CheckStateTransitions(stateData);

            ChangeState(newState);
        }

        private void ChangeState(State state)
        {
            if (state != stateData.currentState && state != null)
            {
                if (stateData.currentState != null)
                {
                    stateData.currentState.ExitState(character);
                }
                stateData.currentState = state;
                stateData.currentDuration = 0;
                state.EnterState(character);
            }
        }
    }
}