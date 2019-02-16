using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AbilitySystem
{
    public class SpellBehaviour : MonoBehaviour
    {

        public CharacterManager target;
        public CharacterManager caster;
        public SpellController controller;
        public Ability ability;
        public float speed;


        public void InitSpell(CharacterManager newCaster, CharacterManager newTarget, 
                              SpellController newController, Ability newAbility, float newSpeed = 5) {
            caster = newCaster;
            target = newTarget;
            controller = newController;
            ability = newAbility;
            speed = newSpeed;
        }



        // Update is called once per frame
        void Update()
        {
            controller.Execute(this);
        }

        private void OnTriggerEnter(Collider other)
        {
            TargetPoint colliderTarget = other.GetComponent<TargetPoint>();

            if (colliderTarget == target)
            {
                target.stats.TakeDamage(5);
                //function for spell disappearing
                World.RemoveObject(transform.gameObject);
            }
        }

    }
}