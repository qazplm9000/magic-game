using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AbilitySystem;



public class CharacterController : MonoBehaviour
{
    public CharacterManager character;
    public bool isPlayer = false;

    public CharacterAI ai;
    public CharacterAction currentAction = CharacterAction.None;
    public Ability nextSkill = null;
    public Vector3 nextLocation;

    public void Awake()
    {
        character = transform.GetComponent<CharacterManager>();
        if (isPlayer) {
            World.inputs.OnInput += ReceiveInput;
        }
    }


    /// <summary>
    /// AI that controls the character
    /// </summary>
    public void CallInput() {
        if (!isPlayer)
        {
            if (ai != null)
            {
                ControlAI();
            }
            //Debug.Log("AI taking action");
        }
        else {
            //Debug.Log("Player taking action");
            MovePlayer();  
        }
    }


    public void ControlAI() {
        switch (currentAction)
        {
            case CharacterAction.Attack:

                break;
            case CharacterAction.Cast:
                break;
            case CharacterAction.SwapPresets:
                break;
            case CharacterAction.Guard:
                break;
            case CharacterAction.Dodge:
                break;
            case CharacterAction.Counter:
                break;
            case CharacterAction.UseItem:
                break;
            case CharacterAction.Move:
                if (character.HasPath())
                {
                    bool reachedDestination = !character.MoveTowardsDestination();

                    if (reachedDestination) {
                        currentAction = CharacterAction.None;
                    }
                }
                else {
                    character.SetDestination(character.GetTarget().transform.position);
                }
                break;
            case CharacterAction.None:
                ai.GetAction(character, this);
                break;
            default:
                currentAction = CharacterAction.None;
                break;
        }
    }



    public void MovePlayer() {
        if (character.ActionAllowed(CharacterAction.Move))
        {
            float horizontal = World.inputs.GetAxis("Horizontal Left");
            float vertical = World.inputs.GetAxis("Vertical Left");

            Vector3 direction = new Vector3(horizontal, 0, vertical);

            character.MoveInDirection(direction);
            character.FaceDirection(direction);
        }
    }




    /// <summary>
    /// Changes whether the character is a player or NPC character
    /// Will properly add/remove itself from the global OnInput functions
    /// </summary>
    /// <param name="isPlayer"></param>
    /// <returns></returns>
    public void UpdateIsPlayerStatus(bool isPlayer)
    {
        if (!this.isPlayer)
        {
            if (isPlayer)
            {
                this.isPlayer = true;
                World.inputs.OnInput += ReceiveInput;
            }
        }
        else
        {
            if (!isPlayer)
            {
                this.isPlayer = false;
                World.inputs.OnInput -= ReceiveInput;
            }
        }
    }





    public void ResetAI() {
        nextSkill = null;
        currentAction = CharacterAction.None;
    }


    public void ReceiveInput(string input) {
        Debug.Log(input);
        switch (input) {
            case "Cast":
                if (character.GetAllowedActions().ActionIsAllowed(CharacterAction.Cast))
                {
                    character.Cast(0);
                    character.currentAction = CharacterAction.Cast;
                }
                break;
            case "Attack":
                if (character.GetAllowedActions().ActionIsAllowed(CharacterAction.Attack))
                {
                    character.currentAction = CharacterAction.Attack;
                }
                break;
            case "Switch Next":
                if (character.GetAllowedActions().ActionIsAllowed(CharacterAction.SwapPresets)) {
                    character.SwitchNextCombo();
                }
                break;
            case "Switch Previous":
                if (character.GetAllowedActions().ActionIsAllowed(CharacterAction.SwapPresets))
                {
                    character.SwitchPreviousCombo();
                }
                break;
        }
    }

}
