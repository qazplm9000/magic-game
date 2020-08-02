using CombatSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EffectSystem
{
    public class EffectManager : MonoBehaviour
    {
        private Combatant user;

        public List<Effect> currentEffects = new List<Effect>();
        public List<float> previousFrames = new List<float>();
        public List<float> currentFrames = new List<float>();

        // Start is called before the first frame update
        void Start()
        {
            user = transform.GetComponent<Combatant>();
        }

        // Update is called once per frame
        void Update()
        {
            int index = 0;

            IncrementTime();

            while (index < currentEffects.Count)
            {
                currentEffects[index].RunEffect(previousFrames[index], currentFrames[index], user);

                if (currentEffects[index].EffectIsFinished(previousFrames[index], currentFrames[index]))
                {
                    RemoveEffectByIndex(index);
                }
                else
                {
                    index++;
                }
            }
        }

        public void AddEffect(Effect effect)
        {
            currentEffects.Add(effect);
            previousFrames.Add(0);
            currentFrames.Add(0);
        }

        private void RemoveEffectByIndex(int index)
        {
            currentEffects.RemoveAt(index);
            previousFrames.RemoveAt(index);
            currentFrames.RemoveAt(index);
        }

        private void IncrementTime()
        {
            for(int i = 0; i < previousFrames.Count; i++)
            {
                previousFrames[i] = currentFrames[i];
                currentFrames[i] += Time.deltaTime;
            }
        }
    }
}