using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace CombatSystem.AI
{
    [CreateAssetMenu(menuName = "AI/Player AI", fileName = "Player AI")]
    public class PlayerAI : CharacterAI
    {

        public string horizontalKeyboardAxis = "Horizontal";
        public string verticalKeyboardAxis = "Vertical";
        public string horizontalControllerAxis = "Controller Horizontal";
        public string verticalControllerAxis = "Controller Vertical";

        public KeyCode castSpellKeyboardKey = KeyCode.T;
        public KeyCode castSpellControllerKey = KeyCode.Joystick1Button3;

        public KeyCode comboKeyboardKey = KeyCode.Return;
        public KeyCode comboControllerKey = KeyCode.Joystick1Button0;

        public KeyCode dodgeKeyboardKey = KeyCode.LeftShift;
        public KeyCode dodgeControllerKey = KeyCode.Joystick1Button1;

        public override void ControlCharacter(CombatantController controller, Combatant character)
        {
            float horizontal = Mathf.Clamp(Input.GetAxis(horizontalKeyboardAxis) + Input.GetAxis(horizontalControllerAxis), -1, 1);
            float vertical = Mathf.Clamp(Input.GetAxis(verticalKeyboardAxis) + Input.GetAxis(verticalControllerAxis), -1, 1);
            Vector3 inputDirection = new Vector3(horizontal, 0, vertical);
            
            Vector3 direction = GetDirectionFromCamera(inputDirection, Camera.main);

            if (direction.magnitude > 1)
            {
                direction.Normalize();
            }

            if (Input.GetKeyDown(castSpellKeyboardKey) || Input.GetKeyDown(castSpellControllerKey)) {
                character.Cast(controller.characterSkill);
            }

            if(Input.GetKeyDown(comboKeyboardKey) || Input.GetKeyDown(comboControllerKey))
            {
                character.Cast(controller.characterCombo);
            }

            if (Input.GetKeyDown(dodgeKeyboardKey) || Input.GetKeyDown(dodgeControllerKey))
            {
                if(direction.magnitude > 0)
                {
                    character.Dodge(direction);
                }
                else
                {
                    character.Guard();
                }
            }

            if(Input.GetKeyUp(dodgeKeyboardKey) || Input.GetKeyUp(dodgeControllerKey))
            {
                character.Unguard();
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                character.Jump(direction);
            }

            character.Move(direction);
        }

        /// <summary>
        /// Takes a Vector3 direction and a reference to the camera to move from
        /// Make the camera 
        /// </summary>
        /// <param name="direction"></param>
        /// <param name="camera"></param>
        /// <returns></returns>
        private Vector3 GetDirectionFromCamera(Vector3 direction, Camera camera)
        {
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