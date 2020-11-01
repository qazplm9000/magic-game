using SkillSystem;
using UnityEngine;

namespace TargettingSystem{
    public interface ITargettable : IDamageable
    {
        Transform GetTransform();
        GameObject GetGameObject();
        string GetName();
	}
}