using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace CombatSystem.AI
{
    [CreateAssetMenu(fileName = "Player Take Turn", menuName = "Character Controller/Take Turn Behaviour/Player Take Turn")]
    public class PlayerTakeTurnBehaviour : TakeTurnBehaviour
    {
        public override void TakeTurn(CombatManager battle, CombatantController controller, Combatant character)
        {
            /*if (Input.GetKeyDown(KeyCode.Tab)) {
                SkillSystem.Skill skill = character.GetSkillList()[0];
                character.Cast(skill);
            }*/

            //Movement controls
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");
            Vector3 inputDirection = new Vector3(horizontal, 0, vertical);

            Vector3 direction = GetDirectionFromCamera(inputDirection, battle.camera);

            if (direction.magnitude > 1) {
                direction.Normalize();
            }

            character.Move(battle, direction);




        }

        
        /// <summary>
        /// Takes a Vector3 direction and a reference to the camera to move from
        /// Make the camera 
        /// </summary>
        /// <param name="direction"></param>
        /// <param name="camera"></param>
        /// <returns></returns>
        private Vector3 GetDirectionFromCamera(Vector3 direction, Camera camera) {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            Vector3 camForward = camera.transform.forward;
            camForward = new Vector3(camForward.x, 0, camForward.z);
            Vector3 camRight = camera.transform.right;
            camRight = new Vector3(camRight.x, 0, camRight.z);

            camForward.Normalize();
            camRight.Normalize();

            Vector3 result = camForward * direction.z + camRight * direction.x;

            return result;
        }
    }
}