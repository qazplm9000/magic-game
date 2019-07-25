using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


namespace CombatSystem.SkillSystem
{
    [CustomEditor(typeof(Skill))]
    public class SkillEditor : Editor
    {

        Skill skill;
        List<SkillEffect> effects;
        List<bool> effectToggled;

        private void OnEnable()
        {
            skill = (Skill)target;

            if (skill.effects == null) {
                skill.effects = new List<SkillEffect>();
            }

            if (skill.effectToggled == null) {
                skill.effectToggled = new List<bool>();
            }

            effects = skill.effects;
            effectToggled = skill.effectToggled;
        }

        public override void OnInspectorGUI()
        {
            Skill skill = (Skill)target;

            base.OnInspectorGUI();

            if (skill.effects == null) {
                skill.effects = new List<SkillEffect>();
            }

            List<SkillEffect> effects = skill.effects;

            for (int i = 0; i < effects.Count; i++) {
                if (effects[i] != null) {
                    effects[i].OnInspectorGUI();
                }
            }
        }

        private void AddEffect() {
            effects.Add(null);
            effectToggled.Add(false);
        }
    }
}