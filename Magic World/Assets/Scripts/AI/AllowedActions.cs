using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace InputSystem
{
    [System.Serializable]
    public class AllowedActions : ISerializationCallbackReceiver
    {
        public class ActionBools {
            public PlayerAction action;
            public bool allowed;
        }

        public List<ActionBools> actions;
        private Dictionary<PlayerAction, bool> actionsDict;





        public void OnAfterDeserialize()
        {
            actionsDict = new Dictionary<PlayerAction, bool>();

            int index = 0;
            while(index < actions.Count) {
                PlayerAction action = actions[index].action;
                bool allowed = actions[index].allowed;

                if (!actionsDict.ContainsKey(action))
                {
                    actionsDict[action] = allowed;
                    index++;
                }
                else {
                    actions.RemoveAt(index);
                }
            }

        }

        public void OnBeforeSerialize()
        {
            actionsDict = new Dictionary<PlayerAction, bool>();

            for (int i = 0; i < actions.Count; i++) {
                PlayerAction action = actions[i].action;
                bool allowed = actions[i].allowed;

                actionsDict[action] = allowed;
            }
        }
    }
}