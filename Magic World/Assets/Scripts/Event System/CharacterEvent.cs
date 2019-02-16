using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace EventSystem
{

    public class CharacterEvent : MonoBehaviour
    {

        public IntEvent characterEvent;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.A)) {
                characterEvent.Invoke("test");
            }
        }

    }
}