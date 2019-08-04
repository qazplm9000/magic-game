using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace CombatSystem
{
    public class TurnName : MonoBehaviour
    {
        private CombatManager battleState;
        private TextMeshProUGUI text;


        // Start is called before the first frame update
        void Start()
        {
            battleState = FindObjectOfType<CombatManager>();
            text = transform.GetComponent<TextMeshProUGUI>();
        }

        // Update is called once per frame
        void Update()
        {
            if (battleState.currentTurn != null)
            {
                text.text = battleState.currentTurn.characterName;
            }
            else {
                text.text = "Error 404";
            }
        }
    }
}