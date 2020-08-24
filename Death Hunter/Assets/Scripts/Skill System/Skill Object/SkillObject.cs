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
        public List<Effect> effects = new List<Effect>();

        public float lifetime = -1;
        public float fadeTime = 1;
        public float fadeSpeed = 2;

        public Timer timer;

        public SkillObjectData(SkillCastData castData, List<Effect> effects)
        {
            this.castData = castData;
            this.effects = effects;
            timer = new Timer();
        }

        public SkillObjectData(SkillCastData castData, List<Effect> effects, float lifetime)
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
        public bool PastTime(float time)
        {
            return timer.PastTime(time);
        }
    }

    public abstract class SkillObject : MonoBehaviour
    {
        public SkillObjectData objData;
        protected List<Material> materials = new List<Material>();
        protected List<float> alphas = new List<float>();

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
                //FadeOut(objData.fadeSpeed);

                if (FadeTimeIsUp())
                {
                    OnExpire();
                    ResetCast();
                }
            }

            
        }

        public void OnTriggerEnter(Collider other)
        {
            Combatant target = other.transform.GetComponent<Combatant>();

            if (target != null)
            {
                if (TargetIsValidTarget(target))
                {
                    OnCombatantCollision(target);
                }
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
            SetupColors();
        }
        
        protected void ApplyEffects(Combatant target)
        {
            List<Effect> effects = objData.effects;
            for (int i = 0; i < effects.Count; i++)
            {
                EffectData data = new EffectData(objData.castData.caster, target, objData.castData.skill.potency, objData.castData.skill.element);
                target.ApplyEffect(effects[i], data);
            }
        }

        protected bool TargetIsValidTarget(Combatant target)
        {
            SkillTargetType tt = objData.castData.skill.GetTargetType();
            bool result = false;

            switch (tt)
            {
                case SkillTargetType.Enemy:
                    result = GetCaster().IsEnemy(target);
                    break;
                case SkillTargetType.Ally:
                    result = GetCaster().IsAlly(target);
                    break;
                case SkillTargetType.Self:
                    break;
                case SkillTargetType.AllyParty:
                    break;
                case SkillTargetType.EnemyParty:
                    break;
            }

            return result;
        }

        protected Combatant GetCaster()
        {
            return objData.castData.caster;
        }

        protected void ResetCast()
        {
            objData = null;
            /*ResetAlphas();
            materials = null;
            alphas = null;*/
            WorldManager.RemoveObject(transform.gameObject);
            OnReset();
        }

        protected void FadeOut(float speed)
        {
            for(int i = 0; i < materials.Count; i++)
            {
                Color thisColor = materials[i].color;
                float newAlpha = thisColor.a - speed * Time.deltaTime;
                Color newColor = new Color(thisColor.r, thisColor.g, thisColor.b, newAlpha);
                materials[i].color = newColor;
                
            }
        }

        private void SetupColors()
        {
            materials = GetAllColors();
            alphas = GetAllAlphas(materials);
        }
        private List<Material> GetAllColors()
        {
            List<Material> materials = new List<Material>();

            Renderer[] renderers = GetComponentsInChildren<Renderer>();
            for(int i = 0; i < renderers.Length; i++)
            {
                Material[] mats = renderers[i].materials;
                materials.AddRange(mats);
            }

            return materials;
        }
        private List<float> GetAllAlphas(List<Material> mats)
        {
            List<float> alphas = new List<float>();

            for (int i = 0; i < mats.Count; i++)
            {
                alphas.Add(mats[i].color.a);
            }

            return alphas;
        }
        private void ResetAlphas()
        {
            for(int i = 0; i < materials.Count; i++)
            {
                Color thisColor = materials[i].color;
                Color newColor = new Color(thisColor.r, thisColor.g, thisColor.b, alphas[i]);
                materials[i].color = newColor;
            }
        }


        private bool LifetimeIsUp()
        {
            return objData.lifetime > 0 && objData.PastTime(objData.lifetime);
        }

        private bool FadeTimeIsUp()
        {
            return objData.PastTime(objData.lifetime + objData.fadeTime);
        }
    }
}