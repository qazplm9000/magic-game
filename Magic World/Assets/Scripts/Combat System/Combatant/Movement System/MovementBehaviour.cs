using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace CombatSystem.MovementSystem
{
    public abstract class MovementBehaviour : ScriptableObject
    {


        public abstract void Move(MovementManager character, Vector3 direction, float speedFraction = 1);
        public abstract void Rotate(MovementManager character, Vector3 direction);


    }
}