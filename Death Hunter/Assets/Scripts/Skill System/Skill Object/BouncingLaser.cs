using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CombatSystem;
using UnityEngine;

namespace SkillSystem
{
    public class BouncingLaser : SkillObject
    {
        public List<Combatant> targetsHit = new List<Combatant>();
        public int bounces = 0;

        protected override void OnCombatantCollision(Combatant target)
        {
            targetsHit.Add(target);
            bounces++;
            ApplyEffects(target);

            objData.castData.target = GetNearestEnemy();
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
            bounces = 0;
        }

        protected override void OnUpdate()
        {
            transform.position += transform.forward * Time.deltaTime * 10;
            transform.LookAt(objData.castData.target.transform.position);
        }

        private Combatant GetNearestEnemy()
        {
            Combatant target = null;
            float nearestDistance = float.PositiveInfinity;

            Combatant[] combatants = FindObjectsOfType<Combatant>();

            for (int i = 0; i < combatants.Length; i++)
            {
                Combatant tempCombatant = combatants[i];

                if (objData.castData.caster.IsEnemy(tempCombatant) && objData.castData.target != tempCombatant)
                {
                    float tempDistance = (transform.position - tempCombatant.transform.position).magnitude;
                    if (targetsHit.Contains(tempCombatant))
                    {
                        tempDistance += 5;
                    }

                    if (tempDistance < nearestDistance)
                    {
                        target = tempCombatant;
                        nearestDistance = tempDistance;
                    }
                }
            }
            return target;
        }
    }
}
