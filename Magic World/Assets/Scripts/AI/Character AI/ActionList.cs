using AbilitySystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ActionList
{
    [SerializeField]
    private List<CharacterAIAction> actions;
    private int currentIndex = 0;
    private CharacterManager target;
    private float time = 0;

    public ActionList() {
        actions = new List<CharacterAIAction>();
    }

    /// <summary>
    /// Returns true while performing actions
    /// Returns false once done
    /// </summary>
    /// <param name="character"></param>
    /// <returns></returns>
    public bool PerformCurrentAction(CharacterManager character) {
        bool acting = true;
        Debug.Log(currentIndex);

        if (currentIndex < actions.Count)
        {
            PerformAction(character, actions[currentIndex]);
        }
        else {
            acting = false;
        }

        return acting;
    }

    public void PerformAction(CharacterManager character, CharacterAIAction action) {
        switch (action.action)
        {
            case CharacterAction.Attack:
                AttackAction(character);
                Debug.Log(character.name + " attacked");
                break;
            case CharacterAction.Cast:
                CastAction(character);
                Debug.Log(character.name + " casted");
                break;
            case CharacterAction.Move:
                MoveAction(character);
                //Debug.Log(character.name + " moved");
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
                WaitAction(character);
                Debug.Log("Waiting");
                break;
        }
    }

    private void AttackAction(CharacterManager character)
    {
        if (character.ActionAllowed(CharacterAction.Attack))
        {
            character.Attack();
        }

        MoveToNextAction();
    }

    private void CastAction(CharacterManager character) {
        if (character.ActionAllowed(CharacterAction.Cast)) {
            //Ability ability = character.GetAbili
            //character.Cast();
            MoveToNextAction();
        }
    }

    private void MoveAction(CharacterManager character) {

        if (character.ActionAllowed(CharacterAction.Move))
        {
            if (!character.HasPath())
            {
                bool pathSet = character.SetDestinationToNearestPoint();

                if (!pathSet) {
                    MoveToNextAction();
                }
            }
            else
            {
                bool reachedDestination = !character.MoveTowardsDestination();

                if (reachedDestination)
                {
                    MoveToNextAction();
                }
            }
        }
        else {
            MoveToNextAction();
        }
    }

    private void WaitAction(CharacterManager character) {
        time += Time.deltaTime;
        if (time > actions[currentIndex].delay) {
            time = 0;
            MoveToNextAction();
        }
    }

    public void AddAction(CharacterAIAction action) {
        actions.Add(new CharacterAIAction(action));
    }

    public List<CharacterAIAction> GetActions() {
        return actions;
    }

    public void MoveToNextAction() {
        currentIndex++;
        time = 0;
    }

    public void ResetList() {
        currentIndex = 0;
    }

}
