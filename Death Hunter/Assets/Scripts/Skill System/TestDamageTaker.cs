using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SkillSystem{
    public class TestDamageTaker : MonoBehaviour, IDamageable
    {

        void Start()
		{
			
		}

		void Update()
		{
			
		}

        public void HealHealth(int healing)
        {
            Debug.Log($"Healed {healing} HP");
        }

        public void TakeDamage(int damage)
        {
            Debug.Log($"Took {damage} damage");
        }
    }
}