using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CombatSystem;
using UnityEngine;

namespace SkillSystem
{
    public class Projectile : SkillObject
    {
        protected override void OnCombatantCollision(Combatant target)
        {
            if (objData.castData.caster.IsEnemy(target))
            {
                ApplyEffects(target);
                ResetCast();
            }
        }

        protected override void OnEnvironmentCollision(GameObject go)
        {
            
        }

        protected override void OnExpire()
        {
            
        }

        protected override void OnReset()
        {
            
        }

        protected override void OnUpdate()
        {
            transform.position += transform.forward * 10 * Time.deltaTime;
            transform.LookAt(objData.castData.intendedTarget.transform);
        }
    }
}
