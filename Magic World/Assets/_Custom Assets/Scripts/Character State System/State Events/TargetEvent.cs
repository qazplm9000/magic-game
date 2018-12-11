using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharacterStateSystem
{
    [CreateAssetMenu(menuName = "Character State/Events/Target Event")]
    public class TargetEvent : StateEvent
    {
        public override void Execute(CharacterManager manager)
        {
            manager.target = manager.targetter.SwitchTarget();
        }
    }
}