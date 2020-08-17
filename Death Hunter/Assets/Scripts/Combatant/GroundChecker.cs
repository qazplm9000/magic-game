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
        public int numOfCollisions = 0;

        private void Awake()
        {
            collider = transform.GetComponent<Collider>();
            character = transform.GetComponentInParent<Combatant>();
        }

        public void LateUpdate()
        {
            if (numOfCollisions == 0)
            {
                character.ChangeFlag(Flag.character_is_grounded, false);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            numOfCollisions++;
            character.ChangeFlag(Flag.character_is_grounded, true);
        }

        private void OnTriggerExit(Collider other)
        {
            numOfCollisions--;
        }
    }
}
