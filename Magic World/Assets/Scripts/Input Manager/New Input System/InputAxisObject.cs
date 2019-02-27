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
        public PlayerInput2 input;

        [HideInInspector]
        public float previousValue = 0;
        [HideInInspector]
        public float currentValue = 0;
        [HideInInspector]
        public float maxValue = 0;

        public void UpdateAxis() {
            previousValue = currentValue;
            currentValue = Input.GetAxis(axis);

            if (timing == KeyTiming.KeyUp) {
                SetMaxValue();
            }
        }

        public void SetMaxValue() {
            if (currentValue > previousValue)
            {
                maxValue = currentValue;
            }
        }

    }
}