using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace AbilitySystem { 
    public class AbilityManager : MonoBehaviour
    {
        protected CharacterManager character;
        protected float previousFrame = 0;
        protected float currentFrame = 0;

        public Ability currentSpell;



        public void Start()
        {
            character = transform.GetComponent<CharacterManager>();
        }


        /// <summary>
        /// Casts the spell
        /// </summary>
        /// <returns></returns>
        public virtual bool Cast() {
            previousFrame = currentFrame;
            currentFrame += Time.deltaTime;

            bool playing = false;

            if (currentSpell != null)
            {
                playing = currentSpell.UseAbility(character, previousFrame, currentFrame);
            }

            if (!playing) {
                ResetTimer();
            }

            return playing;
        }



        /// <summary>
        /// Resets the cast timer
        /// </summary>
        protected void ResetTimer() {
            previousFrame = 0;
            currentFrame = 0;
        }




    }
}