using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CombatSystem;
using UnityEngine;

namespace SkillSystem
{
    public class Hitbox : SkillObject
    {
        public List<Combatant> targetsHit = new List<Combatant>();

        protected override void OnCombatantCollision(Combatant target)
        {
            if (!targetsHit.Contains(target))
            {
                List<SkillEffect> effects = objData.effects;
                for(int i = 0; i < effects.Count; i++)
                {
                    ApplyEffects(target);
                }
                targetsHit.Add(target);
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
            targetsHit = new List<Combatant>();
        }

        protected override void OnUpdate()
        {
            
        }
    }
}
