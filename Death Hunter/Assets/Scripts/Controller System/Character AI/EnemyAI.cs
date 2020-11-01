using System.Collections;
using System.Collections.Generic;
using TargettingSystem;
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
            ITargettable target = character.GetCurrentTarget();

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
                Debug.Log($"Targetting {target.GetName()}");
                if(!TargetIsInLineOfSight(character, target, aggroDistance, aggroAngle))
                {
                    character.Untarget();
                }
            }
        }


        private void MoveTowardsTarget(ITargettable character, ITargettable target)
        {
            var charTrans = character.GetTransform();
            var tarTrans = target.GetTransform();
            Vector3 direction = UnityUtilities.GetVectorBetween(charTrans.position, tarTrans.position);
            Debug.DrawRay(charTrans.up, direction.normalized);
            //character.Move(direction.normalized);
            Debug.Log("Connect enemy movement here");
        }

        private void Attack(Combatant character, CombatantController controller)
        {
            character.Cast(controller.characterCombo);
            controller.ResetTimeSinceLastAttack();
        }

        private bool TargetIsWithinDistance(ITargettable character, ITargettable target, float attackDistance)
        {
            var charPos = character.GetTransform().position;
            var tarPos = target.GetTransform().position;
            return (charPos - tarPos).magnitude < attackDistance;
        }

        private bool IsReadyToAttack(ITargettable character, float timeSinceLastAttack)
        {
            return timeSinceLastAttack > attackCooldown && character.GetFlag(StateSystem.Flag.character_can_cast);
        }

        private bool TargetIsInLineOfSight(ITargettable character, ITargettable target, float maxDistance, float maxAngle)
        {
            var charTrans = character.GetTransform();
            var tarTrans = target.GetTransform();
            bool withinDistance = (charTrans.position - tarTrans.position).magnitude < maxDistance;

            if (withinDistance)
            {
                //Debug.Log("Within distance");
                float angleBetween = Vector3.SignedAngle(charTrans.forward,
                                                        tarTrans.position - charTrans.position,
                                                        charTrans.up);
                bool withinAngle = Mathf.Abs(angleBetween) < maxAngle;
                withinDistance &= withinAngle;
                //Debug.Log($"Within angle: {withinDistance}");
            }

            return withinDistance;
        }
    }
}