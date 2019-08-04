using CombatSystem.SkillSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CombatSystem
{
    [System.Serializable]
    public class Stance
    {

        public Combo combo;
        public List<Skill> skills;
        //public List<Passive> passives;

        public Stance(Combo newCombo, List<Skill> newSkills) {
            combo = newCombo;
            skills = newSkills;
        }
        

        public List<Skill> GetSkillLst() {
            return skills;
        }

        public Combo GetCombo() {
            return combo;
        }
    }
}