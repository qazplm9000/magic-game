using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace TargettingSystem
{
    public class TargetManager : MonoBehaviour
    {
        private CharacterManager character;

        public List<CharacterManager> enemies;
        public List<CharacterManager> allies;

        public CharacterManager target {
            get {
                return _target;
            }
            set {
                _target = value;
                character.target = value;
                character.RaiseEvent("OnTargetChanged");
            }
        }
        private CharacterManager _target;
        public int enemyTargetIndex = 0;
        public int friendlyTargetIndex = 0;
        public bool targetIsEnemy = true;



        public bool highestCriteriaFirst = false;

        public TargetCriteria targetCriteria;



        public void Start()
        {
            character = transform.GetComponent<CharacterManager>();
            InitTargetLists();
        }

        public void OnEnable()
        {
            try
            {
                InitTargetLists();
            }
            catch (System.Exception e) {

            }
        }

        public void OnDisable()
        {
            enemies = null;
            allies = null;
        }




        public CharacterManager GetTarget(bool isEnemy = true)
        {
            CharacterManager result = null;

            List<CharacterManager> targets = EnemyOrAllyList(isEnemy);
            
            
            float criterion = highestCriteriaFirst ? float.MinValue : float.MaxValue;
            for (int i = 0; i < targets.Count; i++)
            {
                float tempCriterion = targetCriteria.GetCriteria(character, targets[i]);

                if (!highestCriteriaFirst && tempCriterion < criterion)
                {
                    result = targets[i];
                    criterion = tempCriterion;
                    enemyTargetIndex = i;
                }
                else if (highestCriteriaFirst && tempCriterion > criterion)
                {
                    result = targets[i];
                    criterion = tempCriterion;
                    enemyTargetIndex = i;
                }
            }

            target = result;

            return result;
        }

        /// <summary>
        /// Gets the next target in the list
        /// Only the World.enemies list will change in size
        /// Players should never get ejected
        ///     Maybe exception if there are pets or anything of the like
        /// </summary>
        /// <param name="isEnemy"></param>
        /// <returns></returns>
        public CharacterManager GetNextTarget(bool isEnemy = true) {
            List<CharacterManager> targets = EnemyOrAllyList(isEnemy);
            CharacterManager target = null;

            if (isEnemy)
            {
                targets = enemies;
            }
            else {
                targets = allies;
            }

            if (targets.Count > 0)
            {
                enemyTargetIndex = (enemyTargetIndex + 1) % targets.Count;
                target = targets[enemyTargetIndex];
            }
            
            return target;
        }


        private List<CharacterManager> EnemyOrAllyList(bool isEnemy) {
            return isEnemy ? enemies : allies;
        }

        private bool IsPlayer() {
            return transform.tag != "enemy";
        }


        private void InitTargetLists() {
            if (IsPlayer())
            {
                enemies = World.battle.allEnemies;
                allies = World.battle.allAllies;
            }
            else
            {
                enemies = World.battle.allAllies;
                allies = World.battle.allEnemies;
            }

            target = enemies[0];
            Debug.Log(transform.tag + " " + target.tag);
            targetIsEnemy = true;
            enemyTargetIndex = 0;
        }

    }
}