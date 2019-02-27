using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace AbilitySystem {
    public abstract class Ability : ScriptableObject {

        public string spellName = "";
        public int manaCost = 1;
        public AbilityElement spellElement;
        public AbilityType spellType;
        public int spellPower;
        public int staggerPower;
        public DamageFormula damageFormula;

        /// <summary>
        /// Run every frame to cast the ability
        /// Returns true if the ability is still running
        /// Returns false if the ability has finished running
        ///     Overrides: 
        ///         CastStart(CharacterManager character)
        ///         Execute(CharacterManager character, float previousFrame, float currentFrame)
        ///         CastEnd(CharacterManager character)
        /// </summary>
        /// <param name="character"></param>
        /// <param name="previousFrame"></param>
        /// <param name="currentFrame"></param>
        /// <returns></returns>
        public bool UseAbility(CharacterManager character, float previousFrame, float currentFrame) {
            bool running = false;

            //Run start event on first frame
            if (previousFrame == 0) {
                StartCast(character);
            }

            //Execute every frame
            running = Execute(character, previousFrame, currentFrame);

            //Run at the very end once Execute returns false
            if (!running)
            {
                EndCast(character);
            }

            return running;
        }

        /// <summary>
        /// Called on the very first frame right before Execute
        /// </summary>
        /// <param name="character"></param>
        protected virtual void StartCast(CharacterManager character) { }

        /// <summary>
        /// Return true while ability is running
        /// Return false when ability is done running
        /// </summary>
        /// <param name="character"></param>
        /// <returns></returns>
        protected virtual bool Execute(CharacterManager character, float previousFrame, float currentFrame) { return false; }

        /// <summary>
        /// Called on the frame when execution is finished
        /// </summary>
        /// <param name="character"></param>
        protected virtual void EndCast(CharacterManager character) { }


        /// <summary>
        /// Returns the spell's element
        /// Override if anything fancy needs to be done (like checking character buffs and stuff)
        /// </summary>
        /// <returns></returns>
        public virtual AbilityElement GetElement(CharacterManager caster) {
            return spellElement;
        }

        /// <summary>
        /// Sets the position of the object relative to caster
        /// </summary>
        /// <param name="caster"></param>
        /// <param name="go"></param>
        /// <param name="offset"></param>
        public void SetPosition(CharacterManager caster, Transform go, Vector3 offset) {
            go.transform.position = caster.transform.position +
                                        caster.transform.forward * offset.z +
                                        caster.transform.right * offset.x +
                                        caster.transform.up * offset.y;
            go.transform.rotation = caster.transform.rotation;
        }

    }
}