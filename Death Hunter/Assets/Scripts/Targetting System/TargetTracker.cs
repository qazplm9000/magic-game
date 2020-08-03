using CombatSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TargettingSystem
{
    public class TargetTracker : MonoBehaviour
    {
        public Combatant character;
        public List<Combatant> targets = new List<Combatant>();
        private SphereCollider collider;

        private void Awake()
        {
            collider = transform.GetComponent<SphereCollider>();
        }

        public void InitTracker(Combatant character, float radius = 15)
        {
            this.character = character;
            collider.radius = radius;
        }

        private void OnTriggerEnter(Collider other)
        {
            Combatant target = other.transform.GetComponent<Combatant>();

            if (character != null && character.IsEnemy(target))
            {
                targets.Add(target);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            Combatant target = other.transform.GetComponent<Combatant>();

            if (targets.Contains(target))
            {
                targets.Remove(target);
            }
        }

        public void SetColliderRadius(float radius)
        {
            collider.radius = radius;
        }
    }
}