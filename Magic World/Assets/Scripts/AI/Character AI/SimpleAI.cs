using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InputSystem
{
    [CreateAssetMenu(menuName = "AI/Simple AI")]
    public class SimpleAI : CharacterAI
    {
        public ActionList actions;

        public override ActionList GetActionList(CharacterManager character, CharacterController controller)
        {
            ActionList newActions = new ActionList();
            List<CharacterAIAction> allActions = actions.GetActions();

            for (int i = 0; i < allActions.Count; i++) {
                newActions.AddAction(allActions[i]);
            }
            return newActions;
        }
        
    }
}