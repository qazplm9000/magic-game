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
        public List<EffectData> currentData = new List<EffectData>();

        // Start is called before the first frame update
        void Start()
        {
            user = transform.GetComponent<Combatant>();
        }

        // Update is called once per frame
        void Update()
        {
            int index = 0;

            Tick();

            while (index < currentEffects.Count)
            {
                currentEffects[index].RunEffect(currentData[index]);

                if (currentEffects[index].EffectIsFinished(currentData[index]))
                {
                    RemoveEffectByIndex(index);
                }
                else
                {
                    index++;
                }
            }
        }

        public void AddEffect(Effect effect, EffectData data)
        {
            currentEffects.Add(effect);
            currentData.Add(data);
        }

        private void RemoveEffectByIndex(int index)
        {
            currentEffects.RemoveAt(index);
            currentData.RemoveAt(index);
        }

        private void Tick()
        {
            for(int i = 0; i < currentData.Count; i++)
            {
                currentData[i].Tick();
            }
        }
    }
}