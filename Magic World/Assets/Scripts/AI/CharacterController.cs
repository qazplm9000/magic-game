using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AbilitySystem;
using UnityEngine.AI;


public class CharacterController : MonoBehaviour
{
    public CharacterManager character;
    public bool isPlayer = false;

    public CharacterAI ai;
    public CharacterAction currentAction = CharacterAction.None;
    public Ability nextSkill = null;
    public Vector3 nextLocation;


    private NavMeshPath path;
    private int pathIndex = 1;

    [SerializeField]
    private ActionList actions;

    [Tooltip("How far away can the character be before considering a point reached")]
    public float pathSensitivity = 0.1f;
    public float angleSensitivity = 2f;


    public void Awake()
    {
        character = transform.GetComponent<CharacterManager>();
        if (isPlayer) {
            //World.inputs.OnInput += ReceiveInput;
        }
        path = new NavMeshPath();

        actions = null;
    }

    private void Update()
    {
        DebugCorners();
    }


    /// <summary>
    /// AI that controls the character
    /// </summary>
    public void CallInput() {
        ControlAI();
        Debug.Log("Calling input on " + character.name);
        /*if (!isPlayer)
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
        }*/
    }


    public void ControlAI() {
        if (actions == null) {
            actions = ai.GetActionList(character, this);
        }

        bool acting = actions.PerformCurrentAction(character);
        //Debug.Log(character.name + " has " + actions.GetActions().Count + " actions");

        if (!acting) {
            actions = null;
            Debug.Log("Reset actions");
        }

        /*switch (currentAction)
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
                        Debug.Log("Character has reached destination");
                    }
                }
                else {
                    SetDestinationToNearestPoint(character.GetTarget());
                }
                break;
            case CharacterAction.None:
                ai.GetActionList(character, this);
                break;
            default:
                currentAction = CharacterAction.None;
                break;
        }*/
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
                //World.inputs.OnInput += ReceiveInput;
            }
        }
        else
        {
            if (!isPlayer)
            {
                this.isPlayer = false;
                //World.inputs.OnInput -= ReceiveInput;
            }
        }
    }





    public void ResetAI() {
        nextSkill = null;
        currentAction = CharacterAction.None;
    }


    /*public void ReceiveInput(string input) {
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
    }*/






    /********************
        **************
         Path Finding
        **************
    *********************
         */

    
    public bool SetDestinationToNearestPoint(CharacterManager target) {
        Vector3 targetPosition = target.GetNearestPointFromPosition(transform.position);
        float characterSize = character.GetDistanceFromFront();

        Vector3 targetToCharacterDirection = transform.position - target.transform.position;
        targetToCharacterDirection = targetToCharacterDirection / targetToCharacterDirection.magnitude;

        Vector3 destination = targetPosition + targetToCharacterDirection*(characterSize + 0.5f);
        
        bool destinationSet = SetDestination(destination);

        return destinationSet;
    }

    public Vector3 NearestPointFromTarget(CharacterManager target, float distance) {
        return target.GetNearestPointFromPosition(transform.position);
    }


    /// <summary>
    /// Calculates a path to the destination
    /// Returns false if path not possible
    /// </summary>
    /// <param name="destination"></param>
    public bool SetDestination(Vector3 destination)
    {
        RaycastHit characterHit;
        RaycastHit targetHit;

        if (Physics.Raycast(transform.position, -transform.up, out characterHit, 100)) {
            //Debug.Log(characterHit.transform.name);
        }
        if (Physics.Raycast(destination, -transform.up, out targetHit, 100)) {
            //Debug.Log(targetHit.transform.name);
        }

        return NavMesh.CalculatePath(characterHit.point, targetHit.point, NavMesh.AllAreas, path);
    }


    public bool HasPath()
    {
        return path.corners.Length > 0;
    }


    /// <summary>
    /// Returns true if path index < length of corners in path
    /// </summary>
    /// <returns></returns>
    public bool HasReachedPathEnd()
    {
        return pathIndex >= path.corners.Length;
    }

    /// <summary>
    /// Moves towards the destination
    /// Returns false when done
    /// </summary>
    /// <returns></returns>
    public bool MoveTowardsDestination()
    {
        bool hasReachedDestination = false;

        if (HasPath() && !HasReachedPathEnd())
        {
            Vector3 currentDestination = path.corners[pathIndex];

            float angleBetween = AngleFromPoint(currentDestination);
            Rotate(angleBetween);
            angleBetween = AngleFromPoint(currentDestination);

            if (angleBetween < 5) {
                MoveInDirection(transform.forward, DistanceFromPoint(currentDestination));
            }

            if (DistanceFromPoint(currentDestination) <= pathSensitivity && angleBetween < angleSensitivity)
            {
                pathIndex++;
            }
        }
        else
        {
            hasReachedDestination = true;
            path.ClearCorners();
            pathIndex = 1;
        }

        return !hasReachedDestination;
    }


    /// <summary>
    /// Goes to the point
    /// </summary>
    /// <param name="point"></param>
    private void GoToPoint(Vector3 point)
    {
        Vector3 direction = DirectionFromPoint(point);
        character.FaceDirection(direction);
        character.MoveForward();
    }

    private void TurnTowardsDirection(Vector3 direction) {
        character.FaceDirection(direction);
    }


    private Vector3 DirectionFromPoint(Vector3 point) {
        return point - transform.position;
    }

    private float DistanceFromPoint(Vector3 point) {
        Vector3 distanceVector = transform.position - point;
        distanceVector.y = 0;
        return distanceVector.magnitude;
    }

    private float AngleFromPoint(Vector3 point) {
        Vector3 direction = DirectionFromPoint(point);
        direction.y = 0;
        Vector3 forward = transform.forward;
        forward.y = 0;

        return Vector3.SignedAngle(forward, direction, Vector3.up);
    }

    private void MoveInDirection(Vector3 direction, float distance) {
        character.MoveInDirection(direction, distance);
    }

    private void Rotate(float angle) {
        character.Rotate(angle);
    }


    private void DebugCorners() {
        for (int i = 0; i < path.corners.Length; i++)
        {
            Debug.DrawRay(path.corners[i], Vector3.up * 3, Color.blue);
        }
    }

}
