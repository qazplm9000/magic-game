using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace EventSystem
{

    [System.Serializable]
    public class CharacterEventHandler
    {
        public delegate void CharacterEvent();
        public event CharacterEvent characterEvent;



        public void Raise() {
            if (characterEvent != null) {
                characterEvent();
            }
        }

        public void SubscribeToEvent(CharacterEvent method) {
            characterEvent += method;
        }

        public void UnsubscribeFromEvent(CharacterEvent method) {
            characterEvent -= method;
        }



    }
}