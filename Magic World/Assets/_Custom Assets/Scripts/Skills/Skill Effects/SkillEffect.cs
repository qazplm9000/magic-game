using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SkillSystem
{
    public abstract class SkillEffect : ScriptableObject
    {
        public abstract bool Execute(CharacterManager user, CharacterManager target, SkillEffectData data);
    }
}