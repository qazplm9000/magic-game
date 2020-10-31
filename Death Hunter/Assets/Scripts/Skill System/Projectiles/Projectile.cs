using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SkillSystem
{
    public enum ProjectileState
    {
        Active,
        FadingOut,
        Inactive
    }
    public abstract class Projectile : MonoBehaviour
    {
        protected ProjectileData data;
        protected Timer timer;
        protected Rigidbody rb;

        private ProjectileState state = ProjectileState.Inactive;

        // Start is called before the first frame update
        void Awake()
        {
            rb = transform.GetComponent<Rigidbody>();
        }

        // Update is called once per frame
        void Update()
        {
            timer.Tick();
            RunState();
        }

        private void RunState()
        {
            switch (state)
            {
                case ProjectileState.Active:
                    UpdateProjectile();
                    if (HasFinishedRunning()) ChangeState(ProjectileState.FadingOut);
                    break;
                case ProjectileState.FadingOut:
                    FadeOutProjectile();
                    if (HasFinishedFading()) ChangeState(ProjectileState.Inactive);
                    break;
                case ProjectileState.Inactive:
                    break;
            }
        }

        private void ChangeState(ProjectileState newState)
        {
            switch (newState)
            {
                case ProjectileState.Active:
                    timer = new Timer();
                    StartProjectile();
                    break;
                case ProjectileState.FadingOut:
                    timer = new Timer();
                    OnFadeProjectile();
                    break;
                case ProjectileState.Inactive:
                    OnFadeFinish();
                    DestroyProjectile();
                    break;
            }

            state = newState;
        }



        public void OnCollisionEnter(Collision collision)
        {
            IDamageable targetHit = collision.transform.GetComponent<IDamageable>();
            Vector3 collisionPosition = collision.GetContact(0).point;

            if (state == ProjectileState.Active)
            {
                if (targetHit != null/* && data.IsValidTarget(targetHit)*/)
                {
                    OnCasterCollision(targetHit, collisionPosition);
                    if (HasFinishedRunning()) ChangeState(ProjectileState.FadingOut);
                }
                else if(targetHit == null)
                {
                    OnObjectCollision(collision.gameObject, collisionPosition);
                    if (HasFinishedRunning()) ChangeState(ProjectileState.FadingOut);
                }
            }
        }






        // Use this on calls

        public void SetupSkill(ProjectileData data)
        {
            this.data = data;
            ChangeState(ProjectileState.Active);
        }


        // Override these

        protected abstract void StartProjectile();

        protected abstract void UpdateProjectile();

        /// <summary>
        /// Called as soon as a projectile ends
        /// </summary>
        protected abstract void OnFadeProjectile();
        protected abstract void FadeOutProjectile();
        protected abstract void OnFadeFinish();
        protected abstract void OnCasterCollision(IDamageable target, Vector3 contactPosition);
        protected abstract void OnObjectCollision(GameObject obj, Vector3 contactPosition);
        protected abstract bool HasFinishedRunning();
        protected abstract bool HasFinishedFading();


        private void DestroyProjectile()
        {
            gameObject.SetActive(false);
            data = null;
            timer = null;
            transform.position = new Vector3(0, -99999, 0);
        }
    }
}