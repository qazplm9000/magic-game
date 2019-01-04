using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CombatSystem
{
    [System.Serializable]
    public class Hitbox : MonoBehaviour
    {
        
        public float lifetime = 0.3f;
        private float timer = 0f;
        public List<CharacterManager> targets = new List<CharacterManager>();
        private CharacterManager user;
        

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
                transform.gameObject.SetActive(false);
            }
        }

        private void OnTriggerStay(Collider other)
        {
            CharacterManager target = other.transform.GetComponent<CharacterManager>();
            if (target != null && !targets.Contains(target) && target != user)
            {
                targets.Add(target);
                target.stats.TakeDamage(5);
            }
        }

        public void CreateHitbox(CharacterManager caster, float lifetime, Vector3 offset)
        {
            //sets the position of the hitbox relative to the player and parents it
            user = caster;
            transform.position = user.transform.position + offset;
            transform.rotation = user.transform.rotation;

            //sets the lifetime for the hitbox
            this.lifetime = lifetime;
            timer = 0f;

            transform.gameObject.SetActive(true);
        }

        public void CreateHitbox(CharacterManager caster, float lifetime, Vector3 offset, Transform parent)
        {
            //sets the position of the hitbox relative to the player and parents it
            user = caster;
            transform.position = parent.position + offset;
            transform.rotation = parent.rotation;
            transform.parent = parent;

            //sets the lifetime for the hitbox
            this.lifetime = lifetime;
            timer = 0f;

            transform.gameObject.SetActive(true);
        }

        public void ResetHitbox()
        {
            //resets the list of targets obtained and the timer
            targets = new List<CharacterManager>();
            timer = 0f;
        }
    }
}