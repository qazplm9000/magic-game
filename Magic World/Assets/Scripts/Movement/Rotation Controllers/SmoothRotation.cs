using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MovementSystem
{
    [CreateAssetMenu(menuName = "Modules/Movement/Smooth Rotation")]
    public class SmoothRotation : CharacterRotation
    {
        public override void Rotate(MovementManager character, Vector3 direction)
        {
            Vector3 newDirection = new Vector3(direction.x, 0, direction.z);

            if (newDirection.magnitude != 0)
            {
                float angle = Vector3.SignedAngle(character.transform.forward, newDirection, Vector3.up);
                //Debug.Log(angle);
                angle = Mathf.LerpAngle(0, angle, 0.7f);
                character.transform.Rotate(character.transform.up, angle);
            }
        }
    }
}