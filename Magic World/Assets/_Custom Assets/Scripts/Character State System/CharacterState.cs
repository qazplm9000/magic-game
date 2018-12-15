using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharacterStateSystem
{
    [System.Serializable]
    public class CharacterState
    {

        public List<StateEventObject> updateEvents;
        public List<StateEventObject> enterEvents;
        public List<StateEventObject> exitEvents;

        public List<StateTransition> transition;
        
    }
}