using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharacterStateSystem
{
    public class StateTransition : ScriptableObject
    {

        public Condition condition;
        public CharacterState state;
        //include functions for State Transition GUI

    }
}