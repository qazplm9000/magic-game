using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SkillSystem
{
    public class BoltSpellBehaviour : EffectBehaviour
    {
        public float speed = 5f;
        public float turnSpeed = 30f;
        private CharacterManager user;
        private CharacterManager target;
        public GameObject spellEffect;
        private Collider spellCollider;

        private void Start()
        {
            spellCollider = transform.GetComponent<Collider>();
        }

        public override void InitializeEffect(CharacterManager user, CharacterManager target = null)
        {
            this.user = user;
            this.target = target;
            spellCollider.enabled = true;
        }
        
        // Update is called once per frame
        void Update()
        {
            MoveForward();
            //TurnTowardsTarget(target.transform);
        }

        private void OnTriggerEnter(Collider other)
        {
            CharacterManager colliderChar = other.transform.GetComponent<CharacterManager>();

            if (colliderChar != null && colliderChar != user) {
                colliderChar.stats.TakeDamage(10);
                GameObject effectObject = ObjectPool.pool.PullObject(spellEffect, transform);
                effectObject.transform.SetParent(null);
                transform.gameObject.SetActive(false);
                spellCollider.enabled = false;
            }
        }

        private void MoveForward() {
            transform.position += transform.forward * speed * Time.deltaTime;
        }

        private void TurnTowardsTarget(Transform target) {
            Vector3 cross = Vector3.Cross(transform.forward, target.position);
            float angle = Vector3.SignedAngle(transform.forward, target.position, cross);
            Mathf.Lerp(0, angle, turnSpeed * Time.deltaTime);
            transform.Rotate(cross, angle);
        }
    }
}