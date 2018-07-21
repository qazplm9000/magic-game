using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SkillSystem
{
    public abstract class Skill : ScriptableObject
    {

        new public string name = "";
        //public string castAnimationName = "";
        //public string launchAnimationName = "";
        public int power = 10;
        public float castTime = 1f;
        public float useTime = 1f;
        public int manaCost = 10;
        public SkillType skillType = SkillType.Melee;
        public Texture skillIcon;
        //public CastLocation castLocation;
        public List<Condition> castConditions = new List<Condition>();
        public List<SkillEffectContainer> castEffects = new List<SkillEffectContainer>();
        public List<SkillEffectContainer> useEffects = new List<SkillEffectContainer>();

        /// <summary>
        /// Returns true if the spell is valid to cast
        /// </summary>
        /// <param name="user"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public abstract bool StartCast(CharacterManager user, CharacterManager target);

        /// <summary>
        /// Returns true every frame during the cast
        /// Returns false if the spell gets cancelled
        /// </summary>
        /// <param name="user"></param>
        /// <param name="target"></param>
        /// <param name="previousFrame"></param>
        /// <param name="currentFrame"></param>
        /// <returns></returns>
        public abstract bool CastingSkill(CharacterManager user, CharacterManager target, float previousFrame, float currentFrame, bool interrupted = false);

        /// <summary>
        /// Returns true while the skill is being used
        /// Returns false after completing the skill
        /// </summary>
        /// <param name="user"></param>
        /// <param name="target"></param>
        /// <param name="previousFrame"></param>
        /// <param name="currentFrame"></param>
        /// <returns></returns>
        public abstract bool UsingSkill(CharacterManager user, CharacterManager target, float previousFrame, float currentFrame, bool interrupted = false);


    }
    
}