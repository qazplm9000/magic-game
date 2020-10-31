using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SkillSystem{
	public interface IDamageable
	{
		void TakeDamage(int damage);
		void HealHealth(int healing);
	}
}