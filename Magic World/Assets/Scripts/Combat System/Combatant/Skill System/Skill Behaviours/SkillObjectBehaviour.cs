using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace CombatSystem.SkillSystem
{
    public abstract class SkillObjectBehaviour : ScriptableObject
    {

        public virtual void StartBehaviour(SkillObjectController controller) {

        }

        public abstract void Run(SkillObjectController controller);

        public virtual void FinishBehaviour(SkillObjectController controller) {
            controller.Kill();
        }
    }
}