using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpellSystem;

namespace ComboSystem
{
    [CreateAssetMenu(fileName = "Combo", menuName = "Combo/ComboHit")]
    public class ComboHit : ScriptableObject
    {

        //name and points
        public string comboName = "Combo 1";
        public int comboPoints = 1;
        public float lifetime = 0.5f;
        public float lifeTimer = 0f;

        //damage variables
        public AttackType attackType = AttackType.Physical;
        public int attackPower = 50;

        //frame data
        public List<Hitbox> hitboxes = new List<Hitbox>();
        public List<float> hitboxLifetimes = new List<float>();
        public List<Vector3> hitboxOffsets = new List<Vector3>();
        public List<float> hitboxStartTimes = new List<float>();

        //delegates
        public delegate void OnAttack(CombatController user);
        public OnAttack OnAttackStart;
        public OnAttack OnAttackEnd;

        private bool interrupted = false;

        public virtual void DealDamage(CombatController target) {
            target.TakeDamage(attackPower);
        }

        public IEnumerator Attack(CombatController user) {

            user.LockMovement();

            lifeTimer = 0;
            int numOfHitboxes = hitboxes.Count;
            int currentIndex = 0;

            while (lifeTimer < lifetime) {
                float newTime = lifeTimer + Time.deltaTime;

                //breaks out when interrupted
                if (interrupted) {
                    interrupted = false;

                    yield break;
                }

                //does nothing if index out of bounds
                if (currentIndex < numOfHitboxes)
                {
                    //creates the hitbox at the proper time
                    if (hitboxStartTimes[currentIndex] <= newTime && hitboxStartTimes[currentIndex] >= lifeTimer)
                    {
                        float hbLifetime = hitboxLifetimes[currentIndex];
                        Vector3 hbOffset = hitboxOffsets[currentIndex];
                        GameObject hitbox = ObjectPool.pool.PullObject(hitboxes[currentIndex].hitboxObject);
                        hitbox.GetComponent<Hitbox>().CreateHitbox(user, hbLifetime, hbOffset, DealDamage);
                        currentIndex++;
                    }
                }

                //updates the timer
                lifeTimer = newTime;

                yield return null;
                
            }

            user.UnlockMovement();
            
        }

        public void Interrupt() {
            interrupted = true;
        }

    }
}