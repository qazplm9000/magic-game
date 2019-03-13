using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InputSystem
{
    [CreateAssetMenu(menuName = "AI/Simple AI")]
    public class SimpleAI : CharacterAI
    {
        public override void GetAction(CharacterManager character, CharacterController controller)
        {
            
            controller.currentAction = CharacterAction.Move;
        }
        
    }
}