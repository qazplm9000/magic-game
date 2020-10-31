using UnityEngine;
using System.Collections;
using System;
using JetBrains.Annotations;

namespace SkillSystem
{
    [Serializable]
    public class ProjectileData
    {
        [HideInInspector]
        public Caster caster;
        [HideInInspector]
        public IDamageable target;
        [HideInInspector]
        public GameObject offsetObject;
        public Vector3 offset;
        public bool parentToOffset;

        public TargetType targetType;
        
        [Space(20)]
        public float lifetime;
        public float fadeTime;

        [Space(20)]
        public float speed;
        public float rotationSpeed;

        [Space(20)]
        public int potency;
        public Element element;

        public ProjectileData(ProjectileData data)
        {
            offset = data.offset;
            parentToOffset = data.parentToOffset;
            targetType = data.targetType;
            lifetime = data.lifetime;
            fadeTime = data.fadeTime;
            speed = data.speed;
            rotationSpeed = data.rotationSpeed;

            potency = data.potency;
            element = data.element;
        }


        public bool IsValidTarget(Caster target)
        {
            bool result = false;
            switch (targetType)
            {
                case TargetType.Enemy:
                    result = caster.IsEnemy(target);
                    break;
                case TargetType.Ally:
                    result = !caster.IsEnemy(target) && caster != target;
                    break;
                case TargetType.Self:
                    result = caster == target;
                    break;
                case TargetType.All:
                    result = true;
                    break;
            }

            return result;
        }
    }
}