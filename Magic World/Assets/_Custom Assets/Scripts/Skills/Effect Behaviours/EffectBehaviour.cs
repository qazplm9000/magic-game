using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SkillSystem
{
    public abstract class EffectBehaviour : MonoBehaviour
    {
        protected Transform targetLocation;

        public abstract void InitializeEffect(CharacterManager user, CharacterManager target = null);
    }
}