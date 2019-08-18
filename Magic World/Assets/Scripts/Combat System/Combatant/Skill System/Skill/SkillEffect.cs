using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using CombatSystem.CastLocationSystem;

namespace CombatSystem.SkillSystem
{
    [System.Serializable]
    public class SkillEffect
    {
        public EffectType effectType;
        public float startTime;

        //Parameters for playing animation

        public string animationName;

        //Parameters for instantiating spell object

        [Tooltip("The object to be instantiated")]
        public GameObject spellObject;
        //public SpellEffect spellEffect;
        [Tooltip("The body party the object will be instantiated relative to")]
        public CastLocation instantiateLocation;
        [Tooltip("The offset of where the object will be instantiated")]
        public Vector3 offset;
        [Tooltip("Check to parent the spell object to the body part")]
        public bool isParented;
        [Tooltip("Behaviour of the object")]
        public SkillObjectBehaviour behaviour;


        public void RunEffect(CombatManager combat, Combatant caster) {
            switch (effectType)
            {
                case EffectType.InstantiateObject:
                    InstantiateObject(combat, caster);
                    break;
                case EffectType.PlayAnimation:
                    PlayAnimation(combat, caster);
                    break;
                default:
                    Debug.Log("Error: Effect does not have a function!");
                    break;
            }
        }


        /// <summary>
        /// Instantiates the spell object relative to the body part selected
        /// </summary>
        /// <param name="combat"></param>
        /// <param name="caster"></param>
        private void InstantiateObject(CombatManager combat, Combatant caster) {
            GameObject obj = combat.PullObject(spellObject);

            GameObject location = caster.GetBodyPart(instantiateLocation);

            Vector3 forwardVec = caster.transform.forward;
            Vector3 rightVec = caster.transform.right;
            Vector3 upVec = caster.transform.up;

            obj.transform.position = location.transform.position + (forwardVec * offset.z) + (rightVec * offset.x) + (upVec * offset.y);

            if (isParented) {
                obj.transform.SetParent(location.transform);
            }
        }

        private void PlayAnimation(CombatManager combat, Combatant caster) {
            caster.PlayAnimation(animationName);
        }

    }
}