using CombatSystem;
using EffectSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SkillSystem
{
    [System.Serializable]
    public class SkillObjectData
    {
        public SkillCastData castData;
        public List<SkillEffect> effects = new List<SkillEffect>();

        public float lifetime = -1;
        public Timer timer;

        public SkillObjectData(SkillCastData castData, List<SkillEffect> effects)
        {
            this.castData = castData;
            this.effects = effects;
            timer = new Timer();
        }

        public SkillObjectData(SkillCastData castData, List<SkillEffect> effects, float lifetime)
        {
            this.castData = castData;
            this.effects = effects;
            this.lifetime = lifetime;
            timer = new Timer();
        }

        public void Tick()
        {
            timer.Tick();
        }

        public bool AtTime(float time)
        {
            return timer.AtTime(time);
        }

        public bool PastTime(float time) {
            return timer.PastTime(time);
        }
    }


    public abstract class SkillObject : MonoBehaviour
    {
        public SkillObjectData objData;

        private void Awake()
        {
            
        }

        // Start is called before the first frame update
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {
            objData.Tick();
            OnUpdate();

            if (LifetimeIsUp())
            {
                OnExpire();
                ResetCast();
            }
        }

        public void OnTriggerEnter(Collider other)
        {
            Combatant target = other.transform.GetComponent<Combatant>();

            if (target != null)
            {
                OnCombatantCollision(target);
            }
            else
            {
                OnEnvironmentCollision(other.gameObject);
            }
        }

        protected abstract void OnCombatantCollision(Combatant target);
        protected abstract void OnEnvironmentCollision(GameObject go);
        protected abstract void OnUpdate();
        /// <summary>
        /// Called when lifetime of object is up
        /// </summary>
        protected abstract void OnExpire();
        protected abstract void OnReset();

        //Public Functions

        public void StartSkill(SkillObjectData data)
        {
            objData = data;
        }

        protected bool IsValidTarget(Combatant target)
        {
            Debug.Log("Currently does not check if target is valid");
            return true;
        }

        protected void ApplyEffects(Combatant target)
        {
            List<SkillEffect> effects = objData.effects;
            for (int i = 0; i < effects.Count; i++)
            {
                target.ApplyEffect(effects[i].effect);
            }
        }

        protected void ResetCast()
        {
            objData = null;
            gameObject.SetActive(false);
            OnReset();
        }




        private bool LifetimeIsUp()
        {
            return objData.lifetime > 0 && objData.PastTime(objData.lifetime);
        }
    }
}