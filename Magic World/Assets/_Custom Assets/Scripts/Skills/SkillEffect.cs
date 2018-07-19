using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SkillSystem
{
    public abstract class SkillEffect : ScriptableObject
    {

        public GameObject effect;
        public CastLocation castLocation;

        public abstract void CreateEffect(SkillCaster user, SkillCaster target = null);

    }
}