using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace AbilitySystem { 
    public class AbilityManager : MonoBehaviour
    {
        protected CharacterManager character;
        protected float previousFrame = 0;
        protected float currentFrame = 0;
        protected Animator anim;

        /*[Tooltip("Used to swap between different combos")]
        public List<ComboList> listOfCombos;
        [Tooltip("The current combo list that has been selected")]
        public ComboList currentCombo;
        [Tooltip("The index of the current combo")]
        public int currentComboIndex = 0;
        [Tooltip("The index of the current attack in the combo")]
        public int currentAttackIndex = 0;
        */

        [Tooltip("Contains all combos and their corresponding skills")]
        public List<SkillPreset> presets;
        public SkillPreset currentPreset;
        public int currentPresetIndex = 0;
        public int currentComboIndex = 0;

        
        private Ability currentAbility;
        public bool isCombo = false;


        public void Start()
        {
            character = transform.GetComponent<CharacterManager>();
            anim = transform.GetComponentInChildren<Animator>();
            //currentCombo = listOfCombos[currentComboIndex];
            ResetCurrentCombo();
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
            //currentCombo = newCombos;
            ResetCurrentCombo();
        }

        public Combo GetNextAttack()
        {
            currentComboIndex++;
            Combo nextCombo = presets[currentPresetIndex].GetNextCombo();
            return nextCombo;
        }


        /// <summary>
        /// Plays the current ability
        /// Returns true while running
        /// Returns false when done
        /// </summary>
        public void PlayCurrentAbility() {
            
            if (currentAbility != null)
            {
                bool isPlaying = currentAbility.UseAbility(character, previousFrame, currentFrame);
                IncrementTimer();

                if (!isPlaying) {
                    ResetSkill();
                    ResetTimer();
                }
            }
            
        }


        /// <summary>
        /// Returns true when using an ability
        /// </summary>
        /// <returns></returns>
        public bool IsUsingAbility() {
            return currentAbility != null;
        }

        /// <summary>
        /// sets the current ability to the next hit in the combo
        /// </summary>
        /// <returns></returns>
        public void Attack()
        {
            currentAbility = currentPreset.GetNextCombo();
            isCombo = true;
        }

        /// <summary>
        /// Resets the combo
        /// Call this when exiting attack state
        /// </summary>
        public void ResetCombo(ComboList combo)
        {
            combo.ResetCombo();
        }


        public void ResetCurrentCombo() {
            currentPreset.combo.ResetCombo();
        }


        /// <summary>
        /// Adds to the index of the preset
        /// Automatically loops around the list
        /// </summary>
        /// <param name="increment"></param>
        public void IncrementPreset(int increment) {
            currentPresetIndex += increment;

            if (presets.Count > 0) {
                currentPresetIndex %= presets.Count;
            }

            if (currentPresetIndex < 0) {
                currentPresetIndex += presets.Count;
            }

            SetCurrentPreset(currentPresetIndex);
            ResetCurrentCombo();
        }


        /// <summary>
        /// Sets the current preset to the one located at index
        /// </summary>
        /// <param name="index"></param>
        public void SetCurrentPreset(int index) {
            currentPreset = presets[index];
        }


        /// <summary>
        /// Returns true if ability used is a combo
        /// </summary>
        /// <returns></returns>
        public bool IsCombo() {
            return isCombo;
        }

        //*************************************************//
        //*************************************************//
        //*************                    ****************//
        //************  Casting Functions   ***************//
        //*************                    ****************//
        //*************************************************//
        //*************************************************//


        /// <summary>
        /// Selects the skill at the specified index
        /// Will do nothing if current skill is not null
        /// </summary>
        /// <param name="index"></param>
        public void SelectSkill(int index) {
            if (currentAbility == null)
            {
                currentAbility = presets[currentPresetIndex].GetSkillAtIndex(index);
            }
        }


        /// <summary>
        /// Casts the spell
        /// </summary>
        /// <returns></returns>
        /*public virtual bool Cast() {
            previousFrame = currentFrame;
            currentFrame += Time.deltaTime;

            bool playing = false;

            if (currentSkill != null)
            {
                playing = currentSkill.UseAbility(character, previousFrame, currentFrame);
            }

            if (!playing) {
                ResetTimer();
                currentSkill = null;
            }

            return playing;
        }*/



        /// <summary>
        /// Resets the cast timer
        /// </summary>
        protected void ResetTimer() {
            previousFrame = 0;
            currentFrame = 0;
        }

        private void IncrementTimer() {
            previousFrame = currentFrame;
            currentFrame += Time.deltaTime;
        }

        private void ResetSkill() {
            currentAbility = null;
            isCombo = false;
        }

        public void CancelAbility() {
            ResetTimer();
            ResetSkill();
        }

    }
}