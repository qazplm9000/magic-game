using BattleSystem;
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
        
        public DamageFormula damageFormula;


        public void InitSpell(CharacterManager newCaster, CharacterManager newTarget, 
                              SpellController newController, Ability newAbility, float newSpeed = 5) {
            caster = newCaster;
            target = newTarget;
            controller = newController;
            ability = newAbility;
            speed = newSpeed;
            damageFormula = ability.damageFormula;
        }



        // Update is called once per frame
        void Update()
        {
            controller.Execute(this);
        }

        private void OnTriggerEnter(Collider other)
        {
            CharacterManager colliderTarget = other.GetComponent<CharacterManager>();
            BattleManager battleState = World.battle;

            if (colliderTarget != null && colliderTarget != caster)
            {
                int damage = damageFormula.CalculateDamage(caster, colliderTarget, battleState, ability);
                colliderTarget.TakeDamage(damage);
                colliderTarget.StaggerDamage(ability.staggerPower);
                //function for spell disappearing
                World.RemoveObject(transform.gameObject);
            }
        }

    }
}