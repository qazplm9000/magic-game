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
        public int currentPresetIndex = 0;
        public int currentComboIndex = 0;
        
        public Combo currentAttack;
        public Combo nextAttack;
        public Skill currentSkill;
        


        public void Start()
        {
            character = transform.GetComponent<CharacterManager>();
            anim = transform.GetComponentInChildren<Animator>();
            //currentCombo = listOfCombos[currentComboIndex];
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
            //currentCombo = newCombos;
            ResetCombo();
        }

        public Combo GetNextAttack()
        {
            currentComboIndex++;
            Combo nextCombo = presets[currentPresetIndex].GetComboAtIndex(currentComboIndex);
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
            currentComboIndex = 0;
            nextAttack = presets[currentPresetIndex].GetComboAtIndex(currentComboIndex);
        }


        /// <summary>
        /// Switches to the next combo
        /// </summary>
        public void SwitchNextPreset() {
            currentPresetIndex = (currentPresetIndex + 1) % presets.Count;

            ResetCombo();
        }


        /// <summary>
        /// Switches to the previous combo
        /// </summary>
        public void SwitchPreviousCombo() {
            currentPresetIndex--;

            if (currentPresetIndex == -1) {
                currentPresetIndex = presets.Count - 1;
            }

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
        /// Selects the skill at the specified index
        /// Will do nothing if current skill is not null
        /// </summary>
        /// <param name="index"></param>
        public void SelectSkill(int index) {
            if (currentSkill == null)
            {
                currentSkill = presets[currentPresetIndex].GetSkillAtIndex(index);
            }
        }


        /// <summary>
        /// Casts the spell
        /// </summary>
        /// <returns></returns>
        public virtual bool Cast() {
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