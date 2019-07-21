using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CombatSystem
{
    public class TurnTimer : MonoBehaviour
    {
        private BattleManager battleState;
        private Slider timerBar; 

        // Start is called before the first frame update
        void Start()
        {
            battleState = FindObjectOfType<BattleManager>();
            timerBar = transform.GetComponent<Slider>();
        }

        // Update is called once per frame
        void Update()
        {
            timerBar.value = battleState.timeRemaining / battleState.timer;
        }
    }
}