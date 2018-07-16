using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SkillSystem
{
    public class SkillUser : MonoBehaviour
    {


        private bool casting = false;
        public Spell currentSpell = null;
        private float castTimer = 0f;
        private bool interrupted = false;
        public Transform target;
        private CombatController combatController;

        public List<Spell> spells = new List<Spell>();
        public int spellIndex = 0;
        



        private Animator animator;

        // Use this for initialization
        void Start()
        {
            animator = transform.GetComponent<Animator>();
            combatController = transform.GetComponent<CombatController>();
        }
        
        
        //called while spell is first cast
        private IEnumerator CastStart() {
            //instantiate spell effect

            Debug.Log("Starting to cast spell");

            StartCoroutine(CastingSpell());

            yield return null;
        }

        //called every frame while spell is casting
        private IEnumerator CastingSpell()
        {
            Debug.Log("Casting spell");
            while (castTimer < currentSpell.castTime) {
                castTimer += Time.deltaTime;
                yield return null;
                
                //stops the spellcast when interrupted
                if (interrupted) {
                    break;
                }
            }

            if (!interrupted) {
                StartCoroutine(CastEnd());
            }

            yield return null;
        }

        //called once spell is done being casted
        //when casttimer > casttime
        private IEnumerator CastEnd() {
            Debug.Log("Done casting");
            //change this to use an object pooler
            GameObject spellObject = Instantiate(currentSpell.spellObject, transform.position, transform.rotation);

            ResetCast();

            yield return null;
        }


        public bool CastSpell(Spell spell)
        {
            bool casted = false;

            if (!IsCasting()) {

                currentSpell = spell;
                casting = true;
                StartCoroutine(CastStart());
                casted = true;
            }

            return casted;
        }

        public bool IsCasting()
        {
            return casting;
        }





        private void ResetCast()
        {
            casting = false;
            currentSpell = null;
            castTimer = 0;
            interrupted = false;
        }

        public void Interrupt() {
            interrupted = true;
        }

        /// <summary>
        /// Increments the index by amount (defaults to 1)
        /// </summary>
        /// <param name="amount"></param>
        public void IncrementIndex(int amount = 1) {

            if (spells.Count > 0)
            {
                spellIndex += amount;
                spellIndex %= spells.Count;
            }
        }


        /// <summary>
        /// Sets the spellIndex to newIndex
        /// Does nothing if there are no spells in the list
        /// Sets to last spell if index out of range
        /// Sets to first spell if index is negative
        /// </summary>
        /// <param name="newIndex"></param>
        public void SetIndex(int newIndex = 0) {

            //breaks out if there are no spells
            if (spells.Count == 0) {
                return;
            }

            if (newIndex >= spells.Count)
            {
                spellIndex = spells.Count - 1;
            }
            else if (newIndex < 0)
            {
                spellIndex = 0;
            }
            else {
                spellIndex = newIndex;
            }
        }

        public void PlayAnimation(string animationName) {
            combatController.PlayAnimation(animationName);
        }



    }
    

}