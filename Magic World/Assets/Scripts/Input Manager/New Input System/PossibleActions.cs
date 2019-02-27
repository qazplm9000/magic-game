using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace InputSystem
{
    public class PossibleActions : ScriptableObject, ISerializationCallbackReceiver
    {
        /*
        public List<InputAllowed> allowedInputs;

        private Dictionary<CharacterInput2, bool> _allowedInputs;

        public class InputAllowed {
            public CharacterInput2 input;
            public bool allowed;
        }




        private void InitDict() {
            _allowedInputs = new Dictionary<CharacterInput2, bool>();

            for (int i = 0; i < allowedInputs.Count; i++) {
                
            }
        }*/

        public void OnAfterDeserialize()
        {
            throw new System.NotImplementedException();
        }

        public void OnBeforeSerialize()
        {
            throw new System.NotImplementedException();
        }
    }
}