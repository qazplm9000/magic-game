using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AbilitySystem
{
    [CreateAssetMenu(menuName = "Abilities/Spells/Bolt Spell")]
    public class BoltSpell : Skill
    {
        public GameObject spellObject;
        public string animationName;

        [Range(0,2)]
        public float totalTime;
        [Range(0,2)]
        public float throwTime;
        public Vector3 offset;

        public SpellController controller;

        protected override void StartCast(CharacterManager character)
        {
            character.PlayAnimation(animationName);
        }

        protected override bool Execute(CharacterManager character, float previousFrame, float currentFrame)
        {
            if (previousFrame <= throwTime && currentFrame > throwTime) {
                SpellBehaviour spell = World.PullSpellObject(spellObject);

                SetPosition(character, spell.transform, offset);

                spell.InitSpell(character, character.target, controller, this);
            }

            return currentFrame < totalTime;
        }

    }
}