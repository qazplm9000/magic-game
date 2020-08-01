using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StateSystem
{
    [System.Serializable]
    public class StateData
    {
        public State currentState;
        public float currentDuration;
        public FlagManager characterFlags;
    }

    [System.Serializable]
    public class StateFlag
    {
        public Flag flag;
        public bool value;
    }



    public enum ConditionType
    {
        FlagHasValue,
        TimeSinceEnterState
    }



    [System.Serializable]
    public class TransitionCondition
    {
        public ConditionType type;
        public float duration;
        public StateFlag flag;

        public bool ConditionMet(StateData sd)
        {
            bool result = false;

            switch (type)
            {
                case ConditionType.FlagHasValue:
                    result = sd.characterFlags.GetFlag(flag.flag) == flag.value;
                    break;
                case ConditionType.TimeSinceEnterState:
                    result = sd.currentDuration >= duration;
                    break;
            }

            return result;
        }
    }



    [System.Serializable]
    public class StateTransition
    {
        public string toStateName = "";
        [Range(0,10)]
        public int toIndex = 0;
        public List<TransitionCondition> stateConditions = new List<TransitionCondition>();

        public bool ConditionsMet(StateData sd)
        {
            bool result = true;

            for (int i = 0; i < stateConditions.Count; i++)
            {
                if (!stateConditions[i].ConditionMet(sd))
                {
                    result = false;
                    break;
                }
            }

            return result;
        }
    }



    [System.Serializable]
    public class State
    {
        /// <summary>
        /// Flags to set upon entering state
        /// </summary>
        public string stateName;
        public List<StateFlag> flags = new List<StateFlag>();
        public List<StateTransition> transitions = new List<StateTransition>();

        public void RunState(FlagManager characterFlags)
        {

        }

        public void EnterState(FlagManager characterFlags)
        {
            SetFlags(characterFlags);
        }

        public State CheckStateTransitions(StateData sd, List<State> states)
        {
            State result = null;

            for (int i = 0; i < transitions.Count; i++)
            {
                StateTransition currentTransition = transitions[i];
                State toState = states[currentTransition.toIndex];
                
                if (currentTransition.ConditionsMet(sd))
                {
                    result = states[currentTransition.toIndex];
                    break;
                }
            }

            return result;
        }


        private void SetFlags(FlagManager characterFlags)
        {
            for (int i = 0; i < flags.Count; i++)
            {
                characterFlags.SetFlag(flags[i].flag, flags[i].value);
            }
        }
    }




    [CreateAssetMenu(fileName = "Character States", menuName = "Character States/State SO")]
    public class CharacterStateSO : ScriptableObject, ISerializationCallbackReceiver
    {
        public State defaultState;
        [Range(0,10)]
        public int defaultIndex;
        public List<State> states = new List<State>();


        /// <summary>
        /// Checks if the state has changed and returns the next state if so
        /// Otherwise, returns null
        /// </summary>
        /// <param name="characterFlags"></param>
        /// <param name="currentState"></param>
        /// <returns></returns>
        public State CheckStateTransitions(StateData sd)
        {
            return sd.currentState.CheckStateTransitions(sd, states);
        }






        /*
         Serialization
             */

        public void OnAfterDeserialize()
        {
            
        }

        public void OnBeforeSerialize()
        {
            SetupDefaultState();
            SetupTransitionStateNames();
        }

        private void SetupDefaultState()
        {
            if (states.Count > defaultIndex && defaultIndex >= 0)
            {
                defaultState = states[defaultIndex];
            }
            else if (states.Count > 0)
            {
                defaultState = states[0];
            }
            else
            {
                defaultState = null;
            }
        }

        private void SetupTransitionStateNames()
        {

            for(int i = 0; i < states.Count; i++)
            {
                SetupStateTransitionData(states[i]);
            }
        }

        private void SetupStateTransitionData(State state)
        {
            List<StateTransition> transitions = state.transitions;
            for(int i = 0; i < transitions.Count; i++)
            {
                int toIndex = transitions[i].toIndex;
                
                if(toIndex >= 0 && toIndex < states.Count)
                {
                    transitions[i].toStateName = $"-> {states[toIndex].stateName}";
                }
                else
                {
                    transitions[i].toStateName = "X Invalid State";
                }
            }
        }
    }
}