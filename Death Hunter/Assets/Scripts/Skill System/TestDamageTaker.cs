using CombatSystem.StatSystem;
using StateSystem;
using System.Collections;
using System.Collections.Generic;
using TargettingSystem;
using UnityEngine;

namespace SkillSystem{
    public class TestDamageTaker : MonoBehaviour, ITargettable
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

        public StatSnapshot CreateStatSnapshot()
        {
            return new StatSnapshot(new List<Stat>());
        }

        public int GetStat(StatType statType)
        {
            return 0;
        }

        public void ChangeFlag(Flag flag, bool flagValue)
        {
            
        }

        public Transform GetTransform()
        {
            return transform;
        }

        public GameObject GetGameObject()
        {
            return gameObject;
        }

        public bool IsDead()
        {
            return false;
        }

        public string GetName()
        {
            return name;
        }

        public bool GetFlag(Flag flag)
        {
            return false;
        }
    }
}