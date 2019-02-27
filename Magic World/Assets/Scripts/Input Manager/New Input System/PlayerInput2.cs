using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Some Input")]
public class PlayerInput2 : ScriptableObject
{
    [Tooltip("The string for the input")]
    public string inputName;
    [Tooltip("Function from Character Manager")]
    public InputType action;

    public void CallInput(CharacterManager character) {
        switch (action)
        {
            case InputType.Attack:
                character.currentAction = CharacterAction.Attack;
                break;
            case InputType.Cast:
                character.Cast(character.abilityCaster.skill1);
                character.currentAction = CharacterAction.Cast;
                break;
            case InputType.SwitchNext:
                character.SwitchNextCombo();
                break;
            case InputType.SwitchPrevious:
                character.SwitchPreviousCombo();
                break;
            default:
                throw new System.EntryPointNotFoundException("Forgot to assign a function here");
        }
    }
}
