using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharacterStateSystem
{
    [System.Serializable]
    public class StateEventObject
    {
        public Condition condition;
        public StateEvent stateEvent;
    }
}