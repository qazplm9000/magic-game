using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace SOEventSystem
{
    public class VarEventObject<T>
    {
        public VarEventSO<T> eventSO;
        public UnityEvent<T> unityEvent;

        public void SetupEvent()
        {
            eventSO?.SubscribeToEvent(unityEvent.Invoke);
        }

        public void DestroyEvent()
        {
            eventSO?.UnsubscribeFromEvent(unityEvent.Invoke);
        }
    }


    public class VarEventListener<T> : MonoBehaviour
    {
        public List<VarEventObject<T>> eventObjs = new List<VarEventObject<T>>();

        // Start is called before the first frame update
        void Start()
        {
            SetupEvents();
        }

        private void OnDestroy()
        {
            DestroyEvents();
        }




        private void SetupEvents()
        {
            for(int i = 0; i < eventObjs.Count; i++)
            {
                eventObjs[i].SetupEvent();
            }
        }

        private void DestroyEvents()
        {
            for(int i = 0; i < eventObjs.Count; i++)
            {
                eventObjs[i].DestroyEvent();
            }
        }
    }
}