using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AbilitySystem
{
    public class ComboUser : MonoBehaviour
    {

        public ComboTree comboTree;
        public int currentIndex = -1;
        public bool usedLightAttack = true;
        private AbilityCaster caster;
        public float comboBuffer = 0.5f;
        private bool casting = false;
        public Ability currentCombo;
        private float previousFrame = 0;
        private float currentFrame = 0;
        private float bufferTimer = 0f;

        private void Start()
        {
            caster = transform.GetComponent<AbilityCaster>();
        }

        // Update is called once per frame
        void Update()
        {
            if (casting)
            {
                casting = currentCombo.Execute(caster, previousFrame, currentFrame);
                previousFrame = currentFrame;
                currentFrame += Time.deltaTime;
            }
            else {
                bufferTimer += Time.deltaTime;
            }

        }

        public void UseCombo(bool lightCombo = true)
        {
            if (bufferTimer > comboBuffer)
            {
                Reset();
            }

            //Cut off combo if last hit was a heavy attack
            if (!usedLightAttack || casting) {
                return;
            }


            //use light combo
            if (lightCombo)
            {
                if (comboTree.lightCombos.Count > currentIndex)
                {
                    currentCombo = comboTree.lightCombos[currentIndex];
                    casting = true;
                    currentIndex++;
                    bufferTimer = 0;
                    previousFrame = 0;
                    currentFrame = 0;
                }
            }
            else { // use heavy combo
                if (comboTree.heavyCombos.Count > currentIndex) {
                    usedLightAttack = false;
                    currentCombo = comboTree.heavyCombos[currentIndex];
                    casting = true;
                    currentIndex++;
                    bufferTimer = 0;
                    previousFrame = 0;
                    currentFrame = 0;
                }
            }
        }

        public void Reset()
        {
            usedLightAttack = true;
            currentIndex = 0;
            casting = false;
            currentFrame = 0;
            previousFrame = 0;
            bufferTimer = 0;
        }


    }
}