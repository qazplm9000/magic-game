using CombatSystem;
using EffectSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace WeaponSystem
{
    public class WeaponObject : MonoBehaviour
    {
        public Combatant caster;
        public new Collider collider;
        public List<Combatant> targetsHit = new List<Combatant>();
        public List<Effect> effects = new List<Effect>();
        public int potency;
        public Element element;

        public Vector3 previousPosition;
        public Vector3 currentPosition;

        private void Awake()
        {
            caster = transform.GetComponentInParent<Combatant>();
            collider = transform.GetComponent<Collider>();
            DeactivateCollider();
            previousPosition = transform.position;
            currentPosition = transform.position;
        }

        private void Update()
        {
            UpdatePosition();
        }

        private void OnTriggerStay(Collider other)
        {
            CheckHit(other);
        }

        private void OnTriggerExit(Collider other)
        {
            CheckHit(other);
        }




        public void ActivateCollider(List<Effect> effects, int potency)
        {
            collider.enabled = true;
            this.effects.AddRange(effects);
            this.potency = potency;
        }

        public void DeactivateCollider()
        {
            collider.enabled = false;
            effects = new List<Effect>();
            potency = 0;
            targetsHit = new List<Combatant>();
        }





        private void CheckHit(Collider other)
        {
            Combatant target = other.GetComponent<Combatant>();

            if (target != null && caster != target && !targetsHit.Contains(target))
            {
                ApplyEffects(target);
                targetsHit.Add(target);
            }
        }

        private void ApplyEffects(Combatant target)
        {
            for(int i = 0; i < effects.Count; i++)
            {
                EffectData data = new EffectData(caster, target, potency, element);
                target.ApplyEffect(effects[i], data);
            }
        }

        private void UpdatePosition()
        {
            previousPosition = currentPosition;
            currentPosition = transform.position;
        }

        private void InterpolatePosition()
        {

        }
    }
}
