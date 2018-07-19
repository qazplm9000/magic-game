using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InputSystem
{
    [CreateAssetMenu(menuName = "Inputs/Axis")]
    public class InputAxis : ScriptableObject
    {

        public string axisName;
        public string desktopAxis;
        public string controllerAxis;

        public bool invertDesktop;
        public bool invertController;

        /// <summary>
        /// Returns the value of the axis
        /// </summary>
        /// <returns></returns>
        public float GetAxis() {
            float result = 0f;

            float desktopValue = Input.GetAxis(desktopAxis);
            desktopValue = invertDesktop ? -desktopValue : desktopValue;
            float controllerValue = Input.GetAxis(controllerAxis);
            controllerValue = invertController ? -controllerValue : controllerValue;

            result = Mathf.Clamp(desktopValue + controllerValue, -1, 1);
            
            return result;
        }

    }
}