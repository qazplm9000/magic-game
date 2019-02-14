using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharacterStateSystem
{
    public class CharacterStateManager : MonoBehaviour
    {

        public CharacterStateTree stateTree;
        public CharacterState currentState;
        public StateTransition currentTransitions;
    }
}