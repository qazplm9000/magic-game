using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CombatSystem;
using UnityEngine;

namespace TargettingSystem
{
    public class EnemyTargetter : MonoBehaviour, ITargetter
    {
        public ITargettable currentTarget;

        private ITargettable player;

        private void Start()
        {
            FindPlayer();
        }

        public ITargettable GetCurrentTarget()
        {
            return currentTarget;
        }

        public ITargettable TargetAlly(bool targetNext = true)
        {
            return null;
        }

        public ITargettable TargetEnemy(bool targetNext = true)
        {
            currentTarget = player;
            return currentTarget;
        }

        public void Untarget()
        {
            currentTarget = null;
        }





        private void FindPlayer()
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            player = playerObj.GetComponent<ITargettable>();
        }
    }
}
