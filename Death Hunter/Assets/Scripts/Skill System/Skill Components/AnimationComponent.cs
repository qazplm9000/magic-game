using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;

namespace SkillSystem
{
    [Serializable]
    public class AnimationComponent : ISkillComponent
    {
        private int id = 0;
        public string description;
        private bool foldout = false;
        public float startTime = 0;
        public string animationName = "";
        public float crossFadeTime;

        public void RunComponent(SkillCastData castData)
        {
            if (castData.AtTime(startTime))
            {
                castData.caster.PlayAnimation(animationName, crossFadeTime);
            }
        }

        public int GetID()
        {
            return id;
        }

        public void SetID(int newId)
        {
            id = newId;
        }

        public float GetEndTime()
        {
            return startTime;
        }

        public bool IsFinished(SkillCastData castData)
        {
            return castData.PastTime(startTime);
        }


        /* GUI */

        public void ShowGUI()
        {
            foldout = EditorGUILayout.Foldout(foldout, description, true);
            if (foldout)
            {
                EditorGUI.indentLevel++;

                startTime = EditorGUILayout.FloatField("StartTime", startTime);
                crossFadeTime = EditorGUILayout.FloatField("Crossfade", crossFadeTime);
                animationName = EditorGUILayout.TextField("Animation", animationName);

                EditorGUI.indentLevel--;
            }
        }

        public void UpdateDescription()
        {
            description = $"{startTime}s - Play \"{animationName}\" w/ {crossFadeTime}s Cross Fade";
        }

    }
}
