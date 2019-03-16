using AbilitySystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionList
{

    private List<CharacterAIAction> actions;
    private int currentIndex = 0;
    private float time = 0;

    public void PerformCurrentAction(CharacterManager character) {
        switch (actions[currentIndex].action)
        {
            case CharacterAction.Attack:
                AttackAction(character);
                break;
            case CharacterAction.Cast:
                break;
            case CharacterAction.Move:
                break;
            case CharacterAction.Guard:
                break;
            case CharacterAction.Dodge:
                break;
            case CharacterAction.Counter:
                break;
            case CharacterAction.UseItem:
                break;
            case CharacterAction.None:
                break;
        }
    }


    private void AttackAction(CharacterManager character)
    {
        if (character.ActionAllowed(CharacterAction.Attack))
        {
            character.Attack();
        }
    }

    private void CastAction(CharacterManager character) {
        if (character.ActionAllowed(CharacterAction.Cast)) {
            //Ability ability = character.GetAbili
            //character.Cast();
        }
    }


    public void MoveToNextAction() {
        currentIndex++;
        time = 0;
    }

    public void ResetList() {
        currentIndex = 0;
    }

}
