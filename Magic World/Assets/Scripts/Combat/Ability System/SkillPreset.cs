using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace AbilitySystem
{
    [System.Serializable]
    public class SkillPreset
    {
        public ComboList combo;
        public List<Skill> skills;


        public SkillPreset() {
            combo = null;
            skills = new List<Skill>();
        }


        public SkillPreset(ComboList newCombo) {
            combo = newCombo;
            skills = new List<Skill>();
        }

        /// <summary>
        /// Initializes a new preset
        /// Will still make sure skills conform to targetting type
        /// </summary>
        /// <param name="newCombo"></param>
        /// <param name="newSkills"></param>
        public SkillPreset(ComboList newCombo, List<Skill> newSkills) {
            combo = newCombo;
            skills = new List<Skill>();
            
            for (int i = 0; i < newSkills.Count; i++) {
                skills.Add(newSkills[i]);
            }
        }


        /// <summary>
        /// Assigns a new combo to the preset
        /// Will remove contradicting skills if the targetting type is different
        /// </summary>
        /// <param name="newCombo"></param>
        public void AssignCombo(ComboList newCombo) {
            if (newCombo != null) {
                combo = newCombo;

                for (int i = 0; i < skills.Count; i++) {
                    if (!IsValidSkill(combo, skills[i])) {
                        skills.RemoveAt(i);
                    }
                }
            }
        }



        /// <summary>
        /// Assigns the skill to the specified index
        /// </summary>
        /// <param name="newSkill"></param>
        /// <param name="index"></param>
        public void AssignSkill(Skill newSkill, int index) {
            if (index >= 0 && index < skills.Count) {
                if (IsValidSkill(combo, newSkill)) {
                    skills[index] = newSkill;
                }
            }
        }



        /// <summary>
        /// Returns true if the new combo will remove any skills
        /// </summary>
        /// <param name="newCombo"></param>
        /// <returns></returns>
        public bool HasInvalidSkill(ComboList newCombo) {
            bool result = false;

            for (int i = 0; i < skills.Count; i++) {
                if (!IsValidSkill(newCombo, skills[i])) {
                    result = true;
                    break;
                }
            }

            return result;
        }


        /// <summary>
        /// Assigns the skill to the slot of the targetting type is valid
        /// Return values:
        ///     0 - Skill assigned
        ///     1 - index out of range
        ///     2 - mismatched targetting type
        /// </summary>
        /// <param name="newSkill"></param>
        /// <param name="index"></param>
        public bool IsValidSkill(ComboList combo, Skill skill) {
            bool result = true;

            if (skill == null)
            {
                result = true;
            }
            else if (combo == null)
            {
                result = false;
            }
            else if (!IsValidTargettingType(combo, skill)) {
                result = true;
            }

            return result;
        }



        /// <summary>
        /// Returns true if the targetting type is valid
        ///     Either targetTypes for both skill and combo are the same
        ///     Or targetType of skill is simply Self
        /// </summary>
        /// <param name="combo"></param>
        /// <param name="skill"></param>
        /// <returns></returns>
        public bool IsValidTargettingType(ComboList combo, Skill skill) {
            return combo.targetType == skill.targetType || skill.targetType == TargetType.Self;
        }




        /// <summary>
        /// Gets the combo at the specified index
        /// Automatically calls the modulo function
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public Combo GetNextCombo() {
            return combo.GetNextCombo();
        }



        public Skill GetSkillAtIndex(int index) {
            Skill result = null;

            if (index >= 0 && index < skills.Count) {
                result = skills[index];
            }

            return result;
        }
    }
}