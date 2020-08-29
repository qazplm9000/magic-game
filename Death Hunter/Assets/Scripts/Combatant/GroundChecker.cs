using StateSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace CombatSystem
{
    public class GroundChecker : MonoBehaviour
    {
        private Combatant character;
        private new Collider collider;
        public float maxDistance = 0.05f;
        public LayerMask mask;
        public bool hitSomething = false;

        private void Awake()
        {
            collider = transform.GetComponent<Collider>();
            character = transform.GetComponentInParent<Combatant>();
        }



        public void LateUpdate()
        {
            RaycastHit hit;
            Ray ray = new Ray(transform.position, -transform.up);

            bool rayHit = Physics.Raycast(ray, out hit, maxDistance, mask);

            if (rayHit)
            {
                character.ChangeFlag(Flag.character_is_grounded, true);
                hitSomething = true;
            }
            else
            {
                character.ChangeFlag(Flag.character_is_grounded, false);
                hitSomething = false;
            }
        }

    }
}
