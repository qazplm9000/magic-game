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
            throw new NotImplementedException();
        }

        protected override void OnEnvironmentCollision(GameObject go)
        {
            throw new NotImplementedException();
        }

        protected override void OnExpire()
        {
            throw new NotImplementedException();
        }

        protected override void OnReset()
        {
            throw new NotImplementedException();
        }

        protected override void OnUpdate()
        {
            throw new NotImplementedException();
        }
    }
}
