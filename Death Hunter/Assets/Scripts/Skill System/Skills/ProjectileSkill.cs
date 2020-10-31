using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SkillSystem
{
    [CreateAssetMenu(menuName = "Skill/Projectile Skill")]
    public class ProjectileSkill : Skill
    {
        [SerializeField]
        private string animationName;
        [SerializeField]
        [Range(0, 0.5f)]
        private float crossfadeTime;

        /*
        [Space(20)]
        [SerializeField]
        private ParticleSystem particles;
        [SerializeField]
        [Range(0,5f)]
        private float particleStartTime;
        [SerializeField]
        [Range(0, 5f)] private float particleLifetime;
        [SerializeField]
        [Range(0, 2f)]
        private float particleFadeTime;
        */

        [SerializeField]
        private float projStart;
        [SerializeField]
        private ProjectileData projData;
        [SerializeField]
        private Projectile projPrefab;

        [Space(20)]
        [SerializeField]
        private float skillDuration;

        public override bool IsRunning(SkillCastData data)
        {
            return !data.PastTime(skillDuration);
        }

        protected override void StartSkill(SkillCastData data)
        {
            

            data.caster.PlayAnimation(animationName, crossfadeTime);
        }

        protected override void UpdateSkill(SkillCastData data)
        {
            if (data.AtTime(projStart)) CreateProjectile(data);
        }

        protected override void EndSkill(SkillCastData data)
        {

        }

        private void CreateProjectile(SkillCastData data)
        {
            Projectile proj = Instantiate(projPrefab);
            ProjectileData newData = new ProjectileData(projData);
            newData.caster = data.caster;
            newData.target = data.target;
            proj.SetupSkill(newData);
        }
    }
}