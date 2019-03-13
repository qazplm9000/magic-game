using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InputSystem
{
    [System.Serializable]
    public class InputObject2
    {
        public string inputName;
        public KeyTiming timing;
        public KeyCode inputKey;
        //public PlayerInput2 input;
        public bool inputHappened = false;



        /// <summary>
        /// Updates the status of the input
        /// Returns the new value
        /// </summary>
        /// <returns></returns>
        public bool UpdateInput() {
            switch (timing) {
                case KeyTiming.KeyDown:
                    inputHappened = Input.GetKeyDown(inputKey);
                    break;
                case KeyTiming.KeyHeld:
                    inputHappened = Input.GetKey(inputKey);
                    break;
                case KeyTiming.KeyUp:
                    inputHappened = Input.GetKey(inputKey);
                    break;
            }

            return inputHappened;
        }
    }
}