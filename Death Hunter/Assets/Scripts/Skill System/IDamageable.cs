using CombatSystem.StatSystem;
using StateSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SkillSystem{
	public interface IDamageable
	{
		void TakeDamage(int damage);
		void HealHealth(int healing);
		bool IsDead();

		StatSnapshot CreateStatSnapshot();
		int GetStat(StatType statType);
		void ChangeFlag(Flag flag, bool flagValue);
		bool GetFlag(Flag flag);
	}
}