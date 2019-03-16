using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AbilitySystem;

public class CharacterAIAction
{

    public CharacterAction action;
    [Tooltip("Wait time for None nodes")]
    public float delay = 0;
    [Tooltip("Moves within a certain distance away")]
    public float distance = 0;
    [Tooltip("Ability to cast")]
    public Ability ability;


}
