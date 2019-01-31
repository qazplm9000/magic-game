using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TargettingSystem
{
    [CreateAssetMenu(menuName = "Modules/Targetting/Target Nearest")]
    public class TargetNearest : Targetter
    {
        public override CharacterManager GetTarget(CharacterManager character, object args = null)
        {
            CharacterManager result = null;

            List<CharacterManager> targets = null;

            //set targets based on whether character is a player or an enemy
            if (World.world.battle.players.Contains(character))
            {
                targets = World.world.battle.enemies;
            }
            else {
                targets = World.world.battle.players;
            }


            float distance = float.MaxValue;
            for (int i = 0; i < targets.Count; i++)
            {
                float tempDistance = (targets[i].transform.position - character.transform.position).magnitude;

                if (tempDistance < distance)
                {
                    result = targets[i];
                    distance = tempDistance;
                }
            }

            return result;
        }
    }
}