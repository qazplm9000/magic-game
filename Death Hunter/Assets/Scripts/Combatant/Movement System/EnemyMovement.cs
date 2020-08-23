using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace CombatSystem.MovementSystem
{
    public class EnemyMovement : MonoBehaviour, IMovement
    {
        public float maxSpeed = 5;

        public float GetSpeed()
        {
            return 0;
        }

        public void Move(Vector3 direction, float speedFraction = 1)
        {
            
        }

        public void SetMaxSpeed(float newMaxSpeed)
        {
            maxSpeed = newMaxSpeed;
        }
    }
}
