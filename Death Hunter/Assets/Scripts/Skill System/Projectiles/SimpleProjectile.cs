using UnityEngine;
using System.Collections;
using SkillSystem;
using DG.Tweening;
using TargettingSystem;

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
        }

        protected override void UpdateProjectile()
        {
            rb.velocity = transform.forward * data.speed;
            if (data.rotationSpeed != 0 && HasTarget()) TurnTowardsTarget();
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

        private void TurnTowardsTarget()
        {
            ITargettable target = data.target;

            Vector3 tarPos = target.GetTransform().position;
            Vector3 distanceVector = UnityUtilities.GetVectorBetween(transform.position, tarPos);
            Vector3 forward = transform.forward;

            Vector3 cross = Vector3.Cross(forward, distanceVector);
            Debug.DrawRay(transform.position, cross, Color.blue);
            float angleBetween = Vector3.SignedAngle(forward, distanceVector, cross);
            float rotateAngle = Mathf.Min(angleBetween, data.rotationSpeed * Time.deltaTime);
            
            transform.RotateAround(transform.position, cross, rotateAngle);
        }

        private bool HasTarget()
        {
            return data.target != null;
        }

    }
}