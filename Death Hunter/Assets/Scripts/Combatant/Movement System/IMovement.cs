using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace CombatSystem.MovementSystem
{
    public interface IMovement
    {
        void Move(Vector3 direction, float speedFraction = 1);
        float GetSpeed();
        void SetMaxSpeed(float newMaxSpeed);
    }
}
