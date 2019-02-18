using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace EventSystem
{
    public class CharacterEventManager : MonoBehaviour
    {
        public List<string> eventNames;

        private Dictionary<string, CharacterEventHandler> _eventDict;

        public void Awake()
        {
            InitDict();
        }


        public void RaiseEvent(string eventName) {
            if (_eventDict.ContainsKey(eventName))
            {
                _eventDict[eventName].Raise();
            }
            else {
                throw new EventDoesNotExistException();
            }
        }


        /// <summary>
        /// Subscribes a method to an event
        /// IMPORTANT!!! Events are init on Awake
        ///     Subscribe in Start or later to ensure you do not get an error
        /// </summary>
        /// <param name="eventName"></param>
        /// <param name="method"></param>
        public void SubscribeEvent(string eventName, CharacterEventHandler.CharacterEvent method) {
            if (_eventDict.ContainsKey(eventName)) {
                _eventDict[eventName].SubscribeToEvent(method);
            }
            else
            {
                throw new EventDoesNotExistException();
            }
        }


        public void UnsubscribeEvent(string eventName, CharacterEventHandler.CharacterEvent method)
        {
            if (_eventDict.ContainsKey(eventName))
            {
                _eventDict[eventName].UnsubscribeFromEvent(method);
            }
            else
            {
                throw new EventDoesNotExistException();
            }
        }

        /// <summary>
        /// Initializes the dictionary for finding events in the character
        /// </summary>
        private void InitDict() {
            _eventDict = new Dictionary<string, CharacterEventHandler>();
            
            for (int i = 0; i < eventNames.Count; i++) {
                string eventName = eventNames[i];

                if (!_eventDict.ContainsKey(eventName)) {
                    _eventDict[eventName] = new CharacterEventHandler();
                }
            }
        }

        

    }

    public class EventDoesNotExistException : System.Exception {
        public EventDoesNotExistException() {

        }

        public EventDoesNotExistException(string e)
                :base("Event " + e + " does not exist in the current context.") {

        }
    }
}