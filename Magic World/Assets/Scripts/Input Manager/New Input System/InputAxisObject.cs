using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace InputSystem
{
    [System.Serializable]
    public class InputAxisObject
    {
        public string axisName;
        public KeyTiming timing;
        public string axis;

        [HideInInspector]
        public float previousValue = 0;
        [HideInInspector]
        public float currentValue = 0;

        public bool inputHappened = false;


        public bool UpdateAxis() {
            previousValue = currentValue;
            currentValue = Input.GetAxis(axis);

            switch (timing)
            {
                case KeyTiming.KeyDown:
                    inputHappened = previousValue == 0 && currentValue > 0;
                    break;
                case KeyTiming.KeyHeld:
                    inputHappened = currentValue > 0;
                    break;
                case KeyTiming.KeyUp:
                    inputHappened = currentValue < previousValue;
                    break;
            }

            return inputHappened;
        }


    }
}