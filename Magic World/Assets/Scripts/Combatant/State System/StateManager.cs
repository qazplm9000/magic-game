using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace StateSystem
{
    

    public class StateManager : MonoBehaviour
    {
        public StateData stateData;
        public CharacterStateSO states;


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



        /*
            Private functions
             */

        private void SetupStartState()
        {
            stateData.currentState = states.defaultState;
            stateData.currentState.EnterState(stateData.characterFlags);
        }

        private void CheckIfStateChanged()
        {
            State newState = states.CheckStateTransitions(stateData);

            if(newState != null && newState != stateData.currentState)
            {
                stateData.currentState = newState;
                stateData.currentDuration = 0;
                newState.EnterState(stateData.characterFlags);
            }
        }
    }
}