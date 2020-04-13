using CombatSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SkillSystem
{
    public class SkillObject : MonoBehaviour
    {
        private Combatant caster;
        private Combatant target;
        private SkillObjectData data;
        public List<SkillObjectAnimation> animations;
        public List<SkillEffect> effects;

        private float movementSpeed;
        [Tooltip("Speed is in degrees")]
        private float rotationSpeed;

        private float previousFrame;
        private float currentFrame;

        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            if (IsRunning())
            {
                Tick();
                RunSkill();
            }

            if (FinishedRunning()) {
                ResetSkillObject();
            }
        }

        public void OnTriggerEnter(Collider other)
        {
            Combatant collided = other.GetComponent<Combatant>();

            if (collided != caster) {
                Debug.Log("Skill object collided with something");

                RunEffects(SkillEffectTiming.OnCollision);

                if (data.destroyOnCollision)
                {
                    RunEffects(SkillEffectTiming.OnDestroy);
                    gameObject.SetActive(false);
                }
            }

        }

        private void RunSkill() {
            transform.position += transform.forward * data.speed * Time.deltaTime;
            //transform.LookAt(target.transform);
            RunAnimations();
            RunEffects(SkillEffectTiming.AfterDelay);
        }



        public void ResetSkillObject() {
            previousFrame = 0;
            currentFrame = 0;
            caster = null;
            target = null;
            data = null;
            gameObject.SetActive(false);
        }

        public void StartSkill(Combatant caster, Combatant target, SkillObjectData data) {
            this.caster = caster;
            this.target = target;
            this.data = data;
            effects = data.CopyEffects();
            animations = data.CopyAnimations();
            RunEffects(SkillEffectTiming.OnCreate);
        }

        private void Tick() {
            previousFrame = currentFrame;
            currentFrame += Time.deltaTime;
        }

        private bool IsRunning() {
            return data.IsRunning(previousFrame, currentFrame);
        }

        private bool FinishedRunning() {
            return data.IsDone(previousFrame, currentFrame);
        }

        /// <summary>
        /// Returns true when time is between previous and current frame
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        private bool AtTime(float time) {
            return time >= previousFrame && time < currentFrame;
        }

        private bool TimeReached(float time) {
            return currentFrame >= time;
        }



        public void SwapTargetAndCaster() {
            Combatant newTarget = caster;
            caster = target;
            target = newTarget;
        }

        /// <summary>
        /// Copies the skill effects to the object
        /// </summary>
        /// <param name="newEffects"></param>
        private void SetSkillEffects(List<SkillEffect> newEffects) {
            effects = newEffects;
        }


        private void RunEffects(SkillEffectTiming timing) {
            for (int i = 0; i < effects.Count; i++) {
                SkillEffect se = effects[i];

                if (se.GetSkillEffectTiming() == timing) {
                    switch (timing)
                    {
                        case SkillEffectTiming.OnCreate:
                            se.ApplyEffect(this, caster, target);
                            break;
                        case SkillEffectTiming.AfterDelay:
                            if (AtTime(se.delay))
                            {
                                se.ApplyEffect(this, caster, target);
                            }
                            break;
                        case SkillEffectTiming.OnCollision:
                            se.ApplyEffect(this, caster, target);
                            break;
                        case SkillEffectTiming.OnDestroy:
                            se.ApplyEffect(this, caster, target);
                            break;
                    }
                }
            }
        }

        private void RunAnimations() {
            for (int i = 0; i < animations.Count; i++) {
                SkillObjectAnimation anim = animations[i];
                Debug.Log("Looping through animations");
                if(AtTime(anim.delay)) {
                    Debug.Log("Playing a one-time animation");
                    RunSingleAnimation(anim);
                }
                if (TimeReached(anim.delay) && anim.playEveryFrame)
                {
                    Debug.Log("Playing a constant animation");
                    RunAnimation(anim);
                }
            }
        }

        private void RunSingleAnimation(SkillObjectAnimation anim) {
            switch (anim.animationType)
            {
                case SkillObjectAnimationType.MoveForwards:
                    movementSpeed = anim.movementSpeed;
                    break;
                case SkillObjectAnimationType.TurnTowardsTarget:
                    rotationSpeed = anim.rotationSpeed;
                    break;
                case SkillObjectAnimationType.ChangeMovementSpeed:
                    movementSpeed = anim.movementSpeed;
                    break;
                case SkillObjectAnimationType.ChangeRotationSpeed:
                    rotationSpeed = anim.rotationSpeed;
                    break;
                case SkillObjectAnimationType.ChangeMovementAcceleration:
                    break;
            }
        }

        private void RunAnimation(SkillObjectAnimation anim) {
            switch (anim.animationType)
            {
                case SkillObjectAnimationType.MoveForwards:
                    transform.position += transform.forward * movementSpeed * Time.deltaTime;
                    break;
                case SkillObjectAnimationType.TurnTowardsTarget:
                    RotationAnimation();
                    break;
                case SkillObjectAnimationType.ChangeMovementSpeed:
                    movementSpeed = anim.movementSpeed;
                    break;
                case SkillObjectAnimationType.ChangeRotationSpeed:
                    rotationSpeed = anim.rotationSpeed;
                    break;
                case SkillObjectAnimationType.ChangeMovementAcceleration:
                    break;
            }
        }

        private void RotationAnimation() {
            Quaternion finalRotation = new Quaternion();
            float angle = Vector3.Angle(transform.forward, target.transform.position - transform.position);
            Debug.Log($"Angle between: {angle}");

            if (Mathf.Abs(angle) > 170) {
                
                finalRotation = Quaternion.FromToRotation(transform.forward, -transform.right);
            }
            else if (angle > 90)
            {
                finalRotation = Quaternion.FromToRotation(transform.position + transform.forward, transform.position + transform.right);
            }
            else if (angle < -90) {
                finalRotation = Quaternion.FromToRotation(transform.forward, -transform.right);
            }
            else
            {
                finalRotation = Quaternion.FromToRotation(transform.forward, target.transform.position - transform.position);
            }

            Quaternion clampedRotation = Quaternion.RotateTowards(transform.rotation, finalRotation, rotationSpeed * Time.deltaTime);
            transform.rotation = clampedRotation;
        }


        public void SetSpeed(float speed) {
            movementSpeed = speed;
        }
    }
}