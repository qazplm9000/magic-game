using UnityEngine;
using System.Collections;
using SkillSystem;
using DG.Tweening;

namespace SkillSystem
{
    public class SimpleProjectile : Projectile
    {
        public GameObject disappearParticles;
        public GameObject collisionParticles;
        [SerializeField]
        private float fadeTime = 0.5f;
        [SerializeField]
        private float fadeDuration = 1f;
        [SerializeField]
        private float fadeDampTime = 0.5f;

        private bool hasCollided = false;

        private GameObject disappearParticleObject;



        //Run functions

        protected override void StartProjectile()
        {
            hasCollided = false;
            transform.localScale = Vector3.one; 
            rb.velocity = transform.forward * data.speed;
        }

        protected override void UpdateProjectile()
        {
            
        }
        
        protected override void OnFadeProjectile()
        {
            if (disappearParticles != null)
            {
                disappearParticleObject = CreateParticles(disappearParticles, transform.position);
                disappearParticleObject.transform.SetParent(transform);
            }

            var scaleTween = DOTween.To(() => transform.localScale, x => transform.localScale = x, Vector3.zero, fadeTime);
            var velocityTween = DOTween.To(() => rb.velocity, x => rb.velocity = x, Vector3.zero, fadeDampTime);
        }

        protected override void FadeOutProjectile()
        {
            
        }

        protected override void OnFadeFinish()
        {
            disappearParticleObject.transform.parent = null;
        }



        //Collision functions

        protected override void OnCasterCollision(IDamageable target, Vector3 contactPosition)
        {
            target.TakeDamage(data.potency);
            Debug.Log($"Enemy took {data.potency} damage");
            hasCollided = true;

            if (collisionParticles != null) CreateParticles(collisionParticles, contactPosition);
        }

        protected override void OnObjectCollision(GameObject obj, Vector3 contactPosition)
        {
            hasCollided = true;

            if (collisionParticles != null) CreateParticles(collisionParticles, contactPosition);
        }





        //Projectile Run Conditions

        protected override bool HasFinishedRunning()
        {
            return timer.PastTime(data.lifetime) || hasCollided;
        }

        protected override bool HasFinishedFading()
        {
            return timer.PastTime(fadeDuration);
        }






        //Helper functions


        private GameObject CreateParticles(GameObject particles, Vector3 contactPosition)
        {
            GameObject particleObject = Instantiate(particles);
            particleObject.transform.position = contactPosition;

            return particleObject;
        }





    }
}