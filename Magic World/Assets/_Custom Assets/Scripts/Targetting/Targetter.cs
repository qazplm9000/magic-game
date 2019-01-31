using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace TargettingSystem
{
    public abstract class Targetter : ScriptableObject
    {
        public bool highestCriteriaFirst;

        public CharacterManager GetTarget(CharacterManager character)
        {
            CharacterManager result = null;

            List<CharacterManager> targets = null;

            //set targets based on whether character is a player or an enemy
            if (World.world.battle.players.Contains(character))
            {
                targets = World.world.battle.enemies;
            }
            else
            {
                targets = World.world.battle.players;
            }


            float criterion = highestCriteriaFirst ? float.MinValue : float.MaxValue;
            for (int i = 0; i < targets.Count; i++)
            {
                float tempCriterion = GetTargetCriteria(character, targets[i]);

                if (!highestCriteriaFirst && tempCriterion < criterion)
                {
                    result = targets[i];
                    criterion = tempCriterion;
                }
                else if (highestCriteriaFirst && tempCriterion > criterion)
                {
                    result = targets[i];
                    criterion = tempCriterion;
                }
            }

            return result;
        }


        public abstract float GetTargetCriteria(CharacterManager character, CharacterManager target);

    }
}