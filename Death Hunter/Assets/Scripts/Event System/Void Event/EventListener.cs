using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace SOEventSystem
{
    [Serializable]
    public class EventListenObject
    {
        public EventSO eventSO;
        public UnityEvent unityEvent;

        public void SetupEvent()
        {
            eventSO?.SubscribeToEvent(unityEvent.Invoke);
        }

        public void RemoveEvent()
        {
            eventSO?.UnsubscribeFromEvent(unityEvent.Invoke);
        }
    }

    public class EventListener : MonoBehaviour
    {
        public List<EventListenObject> eventObjects = new List<EventListenObject>();

        // Start is called before the first frame update
        void Start()
        {
            SetupEventObjects();
        }

        private void OnDestroy()
        {
            CancelEventObjects();
        }


        private void SetupEventObjects()
        {
            for(int i = 0; i < eventObjects.Count; i++)
            {
                eventObjects[i].SetupEvent();
            }
        }

        private void CancelEventObjects()
        {
            for(int i = 0; i < eventObjects.Count; i++)
            {
                eventObjects[i].RemoveEvent();
            }
        }
    }
}