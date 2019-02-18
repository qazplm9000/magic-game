using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace AbilitySystem
{
    [CreateAssetMenu(menuName = "Abilities/Combos/Magic Combo")]
    public class SpellCombo : Combo
    {
        [Range(0,2)]
        [Tooltip("Time for when spell is throw")]
        public float spellThrowTime;

        [Tooltip("Spell's prefab")]
        public GameObject spellObject;

        [Tooltip("Location relative to caster where the spell will instantiate")]
        public Vector3 offset;

        public SpellController controller;

        protected override void StartCast(CharacterManager character)
        {
            character.PlayAnimation(animationName);
        }

        protected override bool Execute(CharacterManager character, float previousFrame, float currentFrame)
        {
            if (previousFrame < spellThrowTime && currentFrame > spellThrowTime) {
                SpellBehaviour spell = World.PullSpellObject(spellObject);
                spell.InitSpell(character, character.target, controller, this);
                
                SetPosition(character, spell.transform, offset);
            }

            return currentFrame < totalTime;
        }
    }
}