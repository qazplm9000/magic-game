using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnTimerUI : MonoBehaviour
{
    private Slider timerBar;


    // Start is called before the first frame update
    void Start()
    {
        timerBar = transform.GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        timerBar.value = CalculateTurnAmount();
    }


    public float CalculateTurnAmount() {
        float result = World.world.turnTimer / World.world.turnTime;
        return result;
    }
}
