using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SkillSystem
{
    public abstract class Skill : ScriptableObject
    {

        new public string name = "";
        public string castAnimationName = "";
        public string launchAnimationName = "";
        public int power = 10;
        public float castTime = 1f;
        public int manaCost = 10;
        public SkillType skillType = SkillType.Melee;
        public Texture skillIcon;
        public CastLocation castLocation;
        public bool requireTarget = false;

        public abstract bool StartCast(CharacterManager user);

        public abstract bool CastSkill(CharacterManager user, CharacterManager target = null);


    }
    
}