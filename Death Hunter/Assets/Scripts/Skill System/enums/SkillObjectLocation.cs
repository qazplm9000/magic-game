using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace SkillSystem
{
    public enum SkillObjectLocation
    {
        Creator, //Caster if created by caster, Skill Object if created by an object
        Caster,
        Target
    }
}