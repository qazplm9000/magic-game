using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace CombatSystem.SkillSystem
{
    public abstract class SkillEffect
    {
        public SkillEffectBehaviour behaviour;

        public abstract void OnCastStart();
        public abstract void OnCastEnd();
        public abstract void Run();

        public abstract void OnInspectorGUI();

    }
}