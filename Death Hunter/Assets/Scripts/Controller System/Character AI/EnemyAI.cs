using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CombatSystem.AI
{
    [CreateAssetMenu(menuName = "AI/Enemy AI")]
    public class EnemyAI : CharacterAI
    {
        public float attackCooldown = 2;
        public float aggroDistance = 10;
        public float aggroAngle = 30;

        public override void ControlCharacter(CombatantController controller, Combatant character)
        {
            Combatant target = character.GetCurrentTarget();

            if (target != null) {
                if (!TargetIsWithinDistance(character, target, controller.attackDistance))
                {
                    MoveTowardsTarget(character, target);
                }
                else if (IsReadyToAttack(character, controller.timeSinceLastAttack))
                {
                    Attack(character, controller);
                }

                if (!TargetIsWithinDistance(character, target, aggroDistance))
                {
                    character.Untarget();
                }
            }
            else
            {
                target = character.GetNearestTarget();
                Debug.Log($"Targetting {target.name}");
                if(!TargetIsInLineOfSight(character, target, aggroDistance, aggroAngle))
                {
                    character.Untarget();
                }
            }
        }


        private void MoveTowardsTarget(Combatant character, Combatant target)
        {
            Vector3 direction = target.transform.position - character.transform.position;
            Debug.DrawRay(character.transform.position, direction.normalized);
            character.Move(direction.normalized);
        }

        private void Attack(Combatant character, CombatantController controller)
        {
            character.Cast(controller.characterCombo);
            controller.ResetTimeSinceLastAttack();
        }

        private bool TargetIsWithinDistance(Combatant character, Combatant target, float attackDistance)
        {
            return (character.transform.position - target.transform.position).magnitude < attackDistance;
        }

        private bool IsReadyToAttack(Combatant character, float timeSinceLastAttack)
        {
            return timeSinceLastAttack > attackCooldown && character.GetFlag(StateSystem.Flag.character_can_cast);
        }

        private bool TargetIsInLineOfSight(Combatant character, Combatant target, float maxDistance, float maxAngle)
        {
            bool withinDistance = (character.transform.position - target.transform.position).magnitude < maxDistance;

            if (withinDistance)
            {
                //Debug.Log("Within distance");
                float angleBetween = Vector3.SignedAngle(character.transform.forward,
                                                        target.transform.position - character.transform.position,
                                                        character.transform.up);
                bool withinAngle = Mathf.Abs(angleBetween) < maxAngle;
                withinDistance &= withinAngle;
                //Debug.Log($"Within angle: {withinDistance}");
            }

            return withinDistance;
        }
    }
}