using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CombatSystem.SpellSystem
{
    public class Hurtbox : MonoBehaviour
    {

        private Collider collider;
        private List<CharacterManager> targets;
        private CharacterManager caster;
        private float lifetime = 0;
        private Skill skill;

        // Start is called before the first frame update
        void Start()
        {
            collider = transform.GetComponent<Collider>();
            collider.isTrigger = true;
        }


        public void SetHurtbox(CharacterManager caster, Skill skill, Vector3 position, Quaternion rotation, float lifetime) {
            this.caster = caster;
            this.skill = skill;
            transform.position = position;
            transform.rotation = rotation;
            this.lifetime = lifetime;
            transform.gameObject.SetActive(true);
            targets = new List<CharacterManager>();
        }

        public void ResetHurtbox() {
            caster = null;
            transform.position = new Vector3(0, -1000000, 0);
            transform.gameObject.SetActive(false);
        }

        private void OnTriggerEnter(Collider collision)
        {
            CharacterManager target = collision.transform.GetComponent<CharacterManager>();

            if (target != null && caster != null) {
                if (caster.IsEnemy(target) && !targets.Contains(target)) {
                    targets.Add(target);
                    DealDamage(caster, target, skill);
                }
            }
        }

        private void DealDamage(CharacterManager caster, CharacterManager target, Skill skill) {

        }

        // Update is called once per frame
        void Update()
        {
            if (lifetime > 0)
            {
                lifetime -= Time.deltaTime;
            }
            else {
                ResetHurtbox();
            }
        }
    }
}