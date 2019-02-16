using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AbilitySystem
{
    [CreateAssetMenu(menuName = "Abilities/Combos/Melee Combo")]
    public class MeleeCombo : Combo
    {
        
        [Range(0, 2)]
        [Tooltip("How long into the animation the hitbox gets instantiated")]
        public float hitboxStartTime;
        [Range(0, 2)]
        [Tooltip("How long the hitbox lasts for")]
        public float hitboxLifetime;

        [Tooltip("The hitbox's prefab")]
        public GameObject hitbox;

        [Tooltip("Offset of the hitbox in relation to character's forward direction")]
        public Vector3 offset;


        protected override void StartCast(CharacterManager character)
        {
            character.PlayAnimation(animationName);
        }

        protected override bool Execute(CharacterManager character, float previousFrame, float currentFrame)
        {
            if (previousFrame < hitboxStartTime && currentFrame >= hitboxStartTime)
            {
                Hitbox newHitbox = World.PullHitboxObject(hitbox);
                newHitbox.CreateHitbox(character, this, hitboxLifetime);

                SetPosition(character, newHitbox.transform, offset);
            }

            return currentFrame < totalTime;
        }
    }
}