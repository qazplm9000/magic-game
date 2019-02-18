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

        public delegate void InputEvent(CharacterInput2 input);
        public event InputEvent OnInput;

        public CharacterEventManager eventManager;


        public void Start()
        {
            eventManager = transform.GetComponent<CharacterEventManager>();
        }

        public void Update()
        {
            CheckKeyEvents();
        }


        /// <summary>
        /// Goes through all key events and runs them if applicable
        /// </summary>
        public void CheckKeyEvents() {
            for (int i = 0; i < inputs.Count; i++)
            {
                switch (inputs[i].timing)
                {
                    case KeyTiming.KeyDown:
                        if (Input.GetKeyDown(inputs[i].inputKey))
                        {
                            eventManager.RaiseEvent(inputs[i].inputEventName);
                        }
                        break;
                    case KeyTiming.KeyHeld:
                        if (Input.GetKey(inputs[i].inputKey))
                        {
                            eventManager.RaiseEvent(inputs[i].inputEventName);
                        }
                        break;
                    case KeyTiming.KeyUp:
                        if (Input.GetKeyUp(inputs[i].inputKey))
                        {
                            eventManager.RaiseEvent(inputs[i].inputEventName);
                        }
                        break;
                }
            }
        }


        /// <summary>
        /// Run through axes and see if events should be run
        /// </summary>
        public void RunAxes() {
            for (int i = 0; i < axisObjects.Count; i++) {
                InputAxisObject axis = axisObjects[i];
                axis.UpdateAxis();

                switch (axis.timing){
                    case KeyTiming.KeyDown:
                        if (axis.currentValue > 0 && axis.previousValue == 0) {
                            eventManager.RaiseEvent(axis.eventName);
                        }
                        break;
                    case KeyTiming.KeyHeld:
                        if (axis.currentValue >= axis.previousValue && axis.currentValue != 0) {
                            eventManager.RaiseEvent(axis.eventName);
                        }
                        break;
                    case KeyTiming.KeyUp:
                        if (axis.currentValue < axis.previousValue && axis.previousValue == axis.maxValue) {
                            eventManager.RaiseEvent(axis.eventName);
                        }
                        break;
                }
            }
        }


    }
}