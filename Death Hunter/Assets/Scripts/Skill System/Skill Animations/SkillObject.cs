using CombatSystem;
using EffectSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SkillSystem
{
    public class SkillObject : MonoBehaviour
    {
        private Skill skill;
        private Combatant caster;
        private Combatant target;
        public GameObject skillGo;
        public List<SkillObjectAnimation> animations;
        public List<SkillEffect> skillEffects;
        public List<Effect> effects = new List<Effect>();
        private List<Combatant> targetsHit;
        private SphereCollider sphereCollider;
        private SkillObject creatorOverload;
        public SkillAnimation anim;

        private float previousFrame;
        private float currentFrame;

        private void Awake()
        {
            sphereCollider = transform.GetComponent<SphereCollider>();
            sphereCollider.enabled = false;
            targetsHit = new List<Combatant>(5);
        }

        // Start is called before the first frame update
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {
            if (IsRunning())
            {
                RunSkill();
            }else {
                ResetSkillObject();
            }
        }

        public void OnTriggerEnter(Collider other)
        {
            Combatant collided = other.GetComponent<Combatant>();

            if (collided != null) {
                TargetCollided(collided);
            }

        }





        //Public Functions

        public void StartSkill(Skill skill, Combatant caster, Combatant target, SkillAnimation anim)
        {
            this.skill = skill;
            this.caster = caster;
            this.target = target;
            this.anim = anim;
            sphereCollider.enabled = true;
            effects = skill.effects;

            LoadEffects();

            _SetupSkill();
            RunSkillEffects(SkillEffectTiming.OnCreate);
        }


        public void SwapTargetAndCaster()
        {
            Combatant newTarget = caster;
            caster = target;
            target = newTarget;
        }







        // Private Functions

        private void RunSkill()
        {
            Tick();
            RunAnimations();
            RunSkillEffects(SkillEffectTiming.AfterDelay);

            if (anim.moveForwards)
            {
                MoveForwards();
            }
            if (anim.rotateTowardsTarget)
            {
                RotateTowardsTarget();
            }
        }

        public void ResetSkillObject()
        {
            previousFrame = 0;
            currentFrame = 0;
            caster = null;
            target = null;
            creatorOverload = null;
            targetsHit = new List<Combatant>(5);
            skillEffects = new List<SkillEffect>(5);
            gameObject.SetActive(false);
            transform.parent = null;

            sphereCollider.enabled = false;
        }



        private void RunEffects(Combatant target)
        {
            for(int i = 0; i < effects.Count; i++)
            {
                target.ApplyEffect(effects[i]);
            }
        }


        private void RunSkillEffects(SkillEffectTiming timing, Combatant newTarget = null)
        {
            Combatant currentTarget = newTarget == null ? target : newTarget;

            for (int i = 0; i < skillEffects.Count; i++)
            {
                SkillEffect se = skillEffects[i];

                if (se.GetSkillEffectTiming() == timing)
                {
                    _RunEffect(se, timing, currentTarget);
                }
            }
        }

        private void RunAnimations()
        {
            for (int i = 0; i < animations.Count; i++)
            {
                SkillObjectAnimation anim = animations[i];
                if (AtTime(anim.startTime))
                {
                    
                }
            }
        }


        // Helper Functions

        private void _SetupSkill() {
            GameObject obj = skill.GetGameObject(anim.objIndex);
            if (obj != null)
            {
                skillGo = Instantiate(obj);
                skillGo.transform.parent = transform;
                skillGo.transform.position = transform.position;
                skillGo.transform.rotation = transform.rotation;

                Debug.Log("Remember to use a pool system here");
                GameObject castLocation = GetCastLocation();

                SetObjectPosition(castLocation);
                SetObjectRotation(castLocation);
                SetChildScale(skillGo);

                transform.parent = GetParentTransform();
            }
        }


        private void _RunEffect(SkillEffect se, SkillEffectTiming timing, Combatant target)
        {
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


        private void _RunAnimation(SkillObjectAnimation anim)
        {
            switch (anim.animationType)
            {
                case SkillObjectAnimationType.MoveForwards:

                    break;
                case SkillObjectAnimationType.TurnTowardsTarget:

                    break;
                case SkillObjectAnimationType.ChangeMovementAcceleration:
                    break;
            }
        }



        private Transform GetParentTransform()
        {
            Transform result = null;

            switch (anim.parent)
            {
                case SkillObjectParent.NoParent:
                    break;
                case SkillObjectParent.ParentToCaster:
                    result = caster.transform;
                    break;
                case SkillObjectParent.ParentToTarget:
                    result = target.transform;
                    break;
                case SkillObjectParent.ParentToCreator:
                    result = creatorOverload == null ? caster.transform : creatorOverload.transform;
                    break;
            }

            return result;
        }

        private GameObject GetCastLocation()
        {
            GameObject result = null;

            switch (anim.location)
            {
                case SkillObjectLocation.Creator:
                    result = caster.gameObject;
                    break;
                case SkillObjectLocation.Caster:
                    result = caster.gameObject;
                    break;
                case SkillObjectLocation.Target:
                    result = target.gameObject;
                    break;
            }

            return result;
        }


        private void SetChildPosition(GameObject child) {
            child.transform.position = transform.position;
            child.transform.rotation = transform.rotation;
        }

        private void SetObjectPosition(GameObject castLocation)
        {
            transform.position = castLocation.transform.position +
                                 castLocation.transform.forward * anim.positionOffset.z +
                                 castLocation.transform.right * anim.positionOffset.x +
                                 castLocation.transform.up * anim.positionOffset.y;
        }

        private void SetObjectRotation(GameObject castLocation)
        {
            transform.rotation = castLocation.transform.rotation * Quaternion.Euler(-anim.rotationOffset.x, anim.rotationOffset.y, anim.rotationOffset.z);
        }

        private void SetChildScale(GameObject child)
        {
            child.transform.localScale = new Vector3(anim.scale, anim.scale, anim.scale);
        }








        private void Tick() {
            previousFrame = currentFrame;
            currentFrame += Time.deltaTime;
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



        

        /// <summary>
        /// Copies the skill effects to the object
        /// </summary>
        /// <param name="newEffects"></param>
        private void SetSkillEffects(List<SkillEffect> newEffects) {
            skillEffects = newEffects;
        }




        private void MoveForwards() {
            transform.position += transform.forward * anim.movementSpeed * Time.deltaTime;
        }

        private void RotateTowardsTarget() {
            Quaternion finalRotation = Quaternion.LookRotation(target.transform.position - transform.position);
            
            Quaternion clampedRotation = Quaternion.RotateTowards(transform.rotation, finalRotation, anim.rotationSpeed * Time.deltaTime);
            transform.rotation = clampedRotation;
        }

        

        public void SetSpeed(float speed) {
            anim.movementSpeed = speed;
        }



        private void LoadEffects() {
            skillEffects = new List<SkillEffect>();
            for (int i = 0; i < anim.effectIds.Count; i++) {
                int id = anim.effectIds[i];
                //effects.Add(skill.GetSkillEffect(id));
            }
        }


        private void TargetCollided(Combatant target) {
            if (ValidTargetHit(target)) {
                targetsHit.Add(target);
                RunSkillEffects(SkillEffectTiming.OnCollision, target);
                

                if (anim.destroyOnCollision)
                {
                    RunSkillEffects(SkillEffectTiming.OnDestroy);
                    RunEffects(target);
                    gameObject.SetActive(false);
                }
            }
        }

        private bool AnimationIsRunning(float startTime, float duration) {
            return previousFrame >= startTime + duration;
        }

        private bool AnimationIsStarting(float startTime) {
            return previousFrame <= startTime && currentFrame > startTime;
        }

        private bool IsRunning() {
            return anim.lifetime >= previousFrame;
        }

        private bool ValidTargetHit(Combatant target) {
            return !targetsHit.Contains(target) && target != caster;
        }
    }
}