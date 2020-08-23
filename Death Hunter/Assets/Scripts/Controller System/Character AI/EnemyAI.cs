using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CombatSystem.AI
{
    [CreateAssetMenu(menuName = "AI/Enemy AI")]
    public class EnemyAI : CharacterAI
    {
        public float attackCooldown = 2;

        public override void ControlCharacter(CombatantController controller, Combatant combatant)
        {
            Combatant target = controller.currentTarget;

            if((combatant.transform.position - target.transform.position).magnitude > controller.attackDistance)
            {
                Vector3 direction = target.transform.position - combatant.transform.position;
                Debug.DrawRay(combatant.transform.position, direction.normalized);
                combatant.Move(direction.normalized);
            }
            else if(controller.timeSinceLastAttack > attackCooldown && combatant.GetFlag(StateSystem.Flag.character_can_cast))
            {
                combatant.Cast(controller.characterCombo);
                controller.ResetTimeSinceLastAttack();
            }
        }
    }
}