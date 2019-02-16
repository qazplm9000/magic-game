using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AbilitySystem
{
    [System.Serializable]
    public class Hitbox : MonoBehaviour
    {
        
        public float lifetime = 0.3f;
        private float timer = 0f;
        public List<CharacterManager> targets = new List<CharacterManager>();
        private CharacterManager user;
        public Ability ability;
        

        // Use this for initialization
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {
            timer += Time.deltaTime;

            //Once lifetime is up, reset hitbox and set to inactive
            if (timer > lifetime)
            {
                ResetHitbox();
                World.RemoveObject(gameObject);
            }
        }
        

        //Adds all targets colliding into list of targets
        private void OnTriggerEnter(Collider other) {
            Debug.Log("Entered trigger");
            CharacterManager target = other.transform.GetComponent<CharacterManager>();

            if (target != null && target != user && !targets.Contains(target)) {
                targets.Add(target);
                target.TakeDamage(5);
            }
        }

        public void CreateHitbox(CharacterManager caster, Ability abilityUsed, float lifetime)
        {
            //sets the caster
            user = caster;
            ability = abilityUsed;

            //sets the lifetime for the hitbox
            this.lifetime = lifetime;
            timer = 0f;

            transform.gameObject.SetActive(true);
        }

        public void CreateHitbox(CharacterManager caster, Ability abilityUsed, float lifetime, Transform parent)
        {
            CreateHitbox(caster, abilityUsed, lifetime);
            transform.SetParent(parent);
        }

        public void ResetHitbox()
        {
            //resets the list of targets obtained and the timer
            targets = new List<CharacterManager>();
            timer = 0f;
            user = null;
            ability = null;
        }
    }
}