using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SkillSystem
{
    [System.Serializable]
    public class SkillEffectContainer
    {
        public float startTime;
        public float length;
        public SkillEffect effect;
        public SkillEffectData data;
    }
}