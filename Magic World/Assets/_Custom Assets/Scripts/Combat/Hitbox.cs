using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CombatSystem
{
    [System.Serializable]
    public class Hitbox : MonoBehaviour
    {

        public GameObject hitboxObject;
        private GameObject hitbox;
        public float lifetime = 0.3f;
        private float timer = 0f;
        public List<CombatController> targets = new List<CombatController>();
        private Vector3 positionOffset;
        private Transform user;

        public delegate void OnTarget(CombatController target);
        public event OnTarget HitTarget;

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
            CombatController target = other.transform.GetComponent<CombatController>();
            if (target != null && !targets.Contains(target) && target != user)
            {
                targets.Add(target);
                HitTarget(target);
            }
        }

        public void CreateHitbox(Transform newUser, float lifetime, Vector3 offset, OnTarget damageFormula)
        {
            //sets the position of the hitbox relative to the player and parents it
            user = newUser;
            transform.position = user.transform.position + offset;
            transform.rotation = user.transform.rotation;
            transform.SetParent(user.transform);

            //sets the lifetime for the hitbox
            this.lifetime = lifetime;
            
            HitTarget += damageFormula;

            transform.gameObject.SetActive(true);
        }

        public void ResetHitbox()
        {
            //resets the list of targets obtained and the timer
            targets = new List<CombatController>();
            timer = 0f;
            HitTarget = null;
            hitbox = null;
        }
    }
}