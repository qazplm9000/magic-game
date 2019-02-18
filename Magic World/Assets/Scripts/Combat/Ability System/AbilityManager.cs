using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace AbilitySystem { 
    public class AbilityManager : MonoBehaviour
    {
        protected CharacterManager character;
        protected float previousFrame = 0;
        protected float currentFrame = 0;

        [Tooltip("Used to swap between different combos")]
        public List<ComboList> listOfCombos;
        [Tooltip("The current combo list that has been selected")]
        public ComboList currentCombo;
        [Tooltip("The index of the current combo")]
        public int currentComboIndex = 0;
        [Tooltip("The index of the current attack in the combo")]
        public int currentAttackIndex = 0;

        public Ability currentSpell;
        public Combo currentAttack;
        public Combo nextAttack;


        public void Start()
        {
            character = transform.GetComponent<CharacterManager>();
            currentCombo = listOfCombos[currentComboIndex];
            ResetCombo();
        }


        //*************************************************//
        //*************************************************//
        //*************                    ****************//
        //************  Attacking Functions  **************//
        //*************                    ****************//
        //*************************************************//
        //*************************************************//


        public void ChangeCombo(ComboList newCombos)
        {
            currentCombo = newCombos;
            ResetCombo();
        }

        public Combo GetNextAttack()
        {
            currentAttackIndex = (currentAttackIndex + 1) % currentCombo.combos.Count;
            Combo nextCombo = currentCombo.combos[currentAttackIndex];
            return nextCombo;
        }

        /// <summary>
        /// Returns true while combo is playing
        /// Returns false once combo has ended
        /// Automatically increments the combo once the current combo has ended
        /// </summary>
        /// <returns></returns>
        public bool Attack()
        {
            previousFrame = currentFrame;
            currentFrame += Time.deltaTime;

            if (currentAttack == null) {
                currentAttack = nextAttack;
                nextAttack = GetNextAttack();
            }

            bool playing = currentAttack.UseAbility(character, previousFrame, currentFrame);

            if (!playing)
            {
                currentAttack = null;
                ResetTimer();
            }

            return playing;
        }

        /// <summary>
        /// Resets the combo
        /// Call this when exiting attack state
        /// </summary>
        public void ResetCombo()
        {
            currentAttackIndex = 0;
            nextAttack = currentCombo.combos[0];
        }


        /// <summary>
        /// Switches to the next combo
        /// </summary>
        public void SwitchNextCombo() {
            currentComboIndex = (currentComboIndex + 1) % listOfCombos.Count;

            currentCombo = listOfCombos[currentComboIndex];

            ResetCombo();
        }


        /// <summary>
        /// Switches to the previous combo
        /// </summary>
        public void SwitchPreviousCombo() {
            currentComboIndex = currentComboIndex - 1;

            if (currentComboIndex == -1) {
                currentComboIndex = listOfCombos.Count - 1;
            }

            currentCombo = listOfCombos[currentComboIndex];

            ResetCombo();
        }



        //*************************************************//
        //*************************************************//
        //*************                    ****************//
        //************  Casting Functions   ***************//
        //*************                    ****************//
        //*************************************************//
        //*************************************************//





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