using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventSystem;


namespace InputSystem
{
    public class GlobalInputManager : MonoBehaviour
    {

        public ControllerType controller;

        public List<InputObject2> inputs;
        public List<InputAxisObject> axisObjects;
        

        public delegate void OnInputTrue(string input);
        public event OnInputTrue OnInput;
        


        public void Start()
        {
            
        }

        public void Update()
        {
            CheckKeyEvents();
            RunAxes();
        }


        /// <summary>
        /// Goes through all key events and runs them if applicable
        /// </summary>
        public void CheckKeyEvents() {
            for (int i = 0; i < inputs.Count; i++)
            {
                InputObject2 input = inputs[i];

                if (input.UpdateInput()) {
                    RaiseEvent(input.inputName);
                }
            }
        }


        /// <summary>
        /// Run through axes and see if events should be run
        /// </summary>
        public void RunAxes() {
            for (int i = 0; i < axisObjects.Count; i++) {
                InputAxisObject axis = axisObjects[i];

                if (axis.UpdateAxis()) {
                    RaiseEvent(axis.axisName);
                }
            }
        }


        /// <summary>
        /// Returns the current value of the axis
        /// </summary>
        /// <param name="axis"></param>
        /// <returns></returns>
        public float GetAxis(string axis) {
            float result = 0;

            for (int i = 0; i < axisObjects.Count; i++) {
                if (axisObjects[i].axisName == axis) {
                    result = axisObjects[i].currentValue;
                }
            }

            return result;
        }



        private void RaiseEvent(string input) {
            if (OnInput != null) {
                OnInput(input);
            }
        }

    }
}