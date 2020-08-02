using CombatSystem;
using EffectSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{

    public Combatant player;
    public AudioClip audio1;
    public AudioClip audio2;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            player.PlayAudio(audio1);
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            player.PlayAudio(audio2);
        }
    }
}
